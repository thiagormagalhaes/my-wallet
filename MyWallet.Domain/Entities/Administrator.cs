using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class Administrator : Entity
    {
        public string Name { get; private set; }
        public string Cnpj { get; private set; }

        protected Administrator() { }

        public Administrator(AdministratorDto administratorDto)
        {
            Name = administratorDto.Name;
            Cnpj = administratorDto.Cnpj;
        }
    }
}
