using Caliburn.Micro;
using Microsoft.Office.Interop.Outlook;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using SportClub.UI.EventModels;
using SportClub.UI.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SportClub.UI.ViewModels
{
    public class MaterialViewModel: Screen , IHandle<MaterialEvent>
    {

        private readonly IMaterialService _materialService;
        private readonly IEventAggregator _event;
        private Club _club;

        public MaterialViewModel(IMaterialService materialService, IEventAggregator events)
        {
            _materialService = materialService;
            _event = events;
            events.Subscribe(this);
        }

        private BindingList<Material> _materials;

		public BindingList<Material> Materials
		{
			get { return _materials; }
            set
            {
                _materials = value;
                NotifyOfPropertyChange(()=> Materials);
            }
		}

        private Material _selectedMaterial = null;

        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                NotifyOfPropertyChange(()=> SelectedMaterial);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BuyMaterial _selectedCartMaterial;

        public BuyMaterial SelectedCartMaterial
        {
            get { return _selectedCartMaterial; }
            set
            {
                _selectedCartMaterial = value;
                NotifyOfPropertyChange(() => SelectedCartMaterial);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

        private BindingList<BuyMaterial> _cart = new BindingList<BuyMaterial>() ;

        public BindingList<BuyMaterial> Cart 
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _materialQuantity = 1;

		public int MaterialQuantity
		{
			get { return _materialQuantity; }
            set
            {
                _materialQuantity = value;
                NotifyOfPropertyChange(()=>MaterialQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
		}

        public string SubTotal
        {
            get
            {
                decimal total = 0;

                foreach (var material in Cart)
                {
                    total += material.Material.Price * material.Quantity;
                }
                return  total.ToString("C");
            }

        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if (MaterialQuantity > 0 && SelectedMaterial != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddToCart()
        {

            BuyMaterial existBuyMaterial = Cart.FirstOrDefault(x => x.Material == SelectedMaterial);
            if (existBuyMaterial != null)
            {
                existBuyMaterial.Quantity += MaterialQuantity;
            }
            else
            {
                BuyMaterial buyMaterial = new BuyMaterial
                {
                    Material = SelectedMaterial,
                    Quantity = MaterialQuantity
                };

                Cart.Add(buyMaterial);
            }

            MaterialQuantity = 1;
            NotifyOfPropertyChange(()=> SubTotal);
            NotifyOfPropertyChange(() => CanOrderMaterial);
           
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                if (SelectedCartMaterial != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void RemoveFromCart()
        {

            if (SelectedCartMaterial.Quantity > 1 && SelectedCartMaterial.Quantity > MaterialQuantity)
            {
                SelectedCartMaterial.Quantity -= MaterialQuantity;
                
            }
            else
            {
                Cart.Remove(SelectedCartMaterial);
            }
            MaterialQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(()=> CanOrderMaterial );
           
        }

        public bool CanOrderMaterial
        {
            get
            {
                bool output = false;

                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void OrderMaterial()
        {

           
            Guid orderNumber = Guid.NewGuid();
            string body = "Order: " + orderNumber + "\n\n";

            var outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            _MailItem oMailItem = outlookApp.CreateItem(OlItemType.olMailItem);

            oMailItem.To = "onlineshop@Decathlon.be";
            oMailItem.Subject = "Order: " + orderNumber +"  for: " + _club.Name;

            foreach (var material in Cart)
            {

                body += material.CartDisplay+ "\n" ;
            }
            body += "\n Adres: \n" + _club.Address.Street + "  " + _club.Address.Number +"\n"+ _club.Address.PostCode + "\n" + _club.Address.City;
            
            oMailItem.Body = body;

            MessageBoxResult messageBox = MessageBox.Show("Is de bestelling compleet", "Order", MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                oMailItem.Display(true);
                ResetMaterialShop();
                _event.PublishOnUIThread(new MainScreenEvent(_club));
            }
            else if (messageBox == MessageBoxResult.No)
            {
               return;
            }
        }

        public void BackButton()
        {
            ResetMaterialShop();
            _event.PublishOnUIThread(new MainScreenEvent(_club));
        }

        public void ResetMaterialShop()
        {
            Cart = new BindingList<BuyMaterial>();
        }

        public void Handle(MaterialEvent message)
        {
            var sportMaterial = new BindingList<Material>();

            var clubSports = message.Club.Sports.Select(sport => sport.SportId).ToList();

            foreach (var mat in clubSports.Select(sportId => _materialService.GetMaterialForSport(sportId)).SelectMany(sportsMaterials => sportsMaterials))
            {
                sportMaterial.Add(mat);
            }

            Materials = sportMaterial;

            _club = message.Club;

        }
    }
}
