using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SportClub.Data.EntityModels;
using SportClub.UI.Annotations;

namespace SportClub.UI.Models
{
    //ModelClass to work with het shop. 
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

                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Quantity)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CartDisplay)));
            }
        }

        //string to show in shop
        public string CartDisplay => $"{Material.MaterialName} : {Quantity}";

        //to change the amount inside the cart and stay at the same position
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
