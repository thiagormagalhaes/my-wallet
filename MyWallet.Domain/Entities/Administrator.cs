namespace MyWallet.Domain.Entities
{
    public class Administrator : Entity
    {
        public string Name { get; private set; }
        public string Cnpj { get; private set; }

        protected Administrator() { }
    }
}
