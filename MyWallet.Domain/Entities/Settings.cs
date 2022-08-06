using MyWallet.Domain.Dto;

namespace MyWallet.Domain.Entities
{
    public class Settings : Entity
    {
        public bool RegisteredCompanies { get; private set; }

        protected Settings() { }

        public Settings(SettingsDto settingsDto)
        {
            RegisteredCompanies = settingsDto.RegisteredCompanies;
        }
    }
}
