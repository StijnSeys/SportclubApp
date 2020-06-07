using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SportClub.Data.EntityModels;
using SportClub.UI.Annotations;

namespace SportClub.UI.Models
{
    //ModelClass to work with het MaterialShop. 
    public class BuyMaterial : INotifyPropertyChanged
    {
        public Material Material { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;

                //to change the amount inside the cart and let the cartItems stay in the same position
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Quantity)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CartDisplay)));
            }
        }

        //string to show in shop
        public string CartDisplay => $"{Material.MaterialName} : {Quantity}";


        public event PropertyChangedEventHandler PropertyChanged;


    }
}
