using System;


namespace FinalId.Web.Components
{
    public class Message
    {
        public string EncryptedContent { get; set; }

        public string SenderPublicKey { get; set; }

        public string Signature { get; set; }

        public override string ToString() 
        {
            
        }
    }
}
