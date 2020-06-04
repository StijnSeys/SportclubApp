﻿namespace Sportclub.UI.EventModels
{
    public class LoginEvent
  {
      
      public string Text { get; private set; }

      public string ClubName { get; private set; }


      public LoginEvent(string text,string clubName)
      {
          Text = text;
          ClubName = clubName;
      }


  }
}
