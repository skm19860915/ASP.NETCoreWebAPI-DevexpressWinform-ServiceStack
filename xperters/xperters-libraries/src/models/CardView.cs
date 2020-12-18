using System;


namespace xperters.models
{
    public class CardView:BaseView
    { 
        public long? ExpMonth { get; set; }
     
        public long? ExpYear { get; set; }
    
        public string Number { get; set; }
        public string NumberSuffix { get; set; }

        public string AddressCity { get; set; }
      
        public string AddressCountry { get; set; }
        
        public string AddressLine1 { get; set; }
       
        public string AddressLine2 { get; set; }
        
        public string AddressState { get; set; }
       
        public string AddressZip { get; set; }
      
        public string Currency { get; set; }

        public string Name { get; set; }
     
       public Guid UserId { get; set; }
       public UserView User { get; set; }

    }
}
