using System.ComponentModel;
using Caliburn.Micro;

namespace SportClub.UI.ViewModels
{
   public class MaterialViewModel: Screen
    {

		private BindingList<string> _materials;

		public BindingList<string> Materials
		{
			get { return _materials; }
            set
            {
                _materials = value;
                NotifyOfPropertyChange(()=> Materials);
            }
		}

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private string _materialQuantity;

		public string MaterialQuantity
		{
			get { return _materialQuantity; }
            set
            {
                _materialQuantity = value;
                NotifyOfPropertyChange(()=>MaterialQuantity);
            }
		}

      

        public string SubTotal
        {
            get
            {
                return "0.00€";
            }

        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // check for something == true.....

                return output;
            }
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                // check for something == true.....

                return output;
            }
        }

        public void RemoveFromCart()
        {

        }



        public void OrderMaterial()
        {


        }

        public bool CanOrderMaterial
        {
            get
            {
                bool output = false;

                // check for something == true.....

                return output;
            }
        }
    }
}
