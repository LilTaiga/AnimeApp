using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes
{
    //This class manages the creation and deletion of all accounts.
    //It should be used instead of manipulating the UserAccount objects directly.
    public static class AccountManager
    {

        #region Properties

        private static UserAccount account;
        private static bool exists;
        
        public static UserAccount CurrentAccount
        {
            get
            {
                if (!exists)
                    return null;
                return account;
            }
        }

        #endregion



        #region Methods - Account Management

        public static UserAccount CreateAccount(string _name, Anilist.Account _aniAcc)
        {
            UserAccount newAccount = new UserAccount(_name, _aniAcc);

            return newAccount;
        }

        public static void SetCurrentAccount(UserAccount _acc)
        {
            account = _acc;
            exists = true;
        }

        public static void RemoveCurrentAccoutn()
        {
            account = null;
            exists = false;
        }

        public static async Task WriteAccountToDisk()
        {
            if (CurrentAccount == null)
                return;

            var json = Utilities.JsonHandler.FromObjectToJson(CurrentAccount);
            await Utilities.FileHandler.WriteBytesToLocalFolder(CurrentAccount.Name + ".json", json);
        }

        public static async Task LoadAccountFromDisk(string _accName)
        {
            var jsonContent = await Utilities.FileHandler.ReadBytesFromLocalFolder(_accName + ".json");
            var desserializedAcc = Utilities.JsonHandler.FromJsonToObject<UserAccount>(Encoding.UTF8.GetString(jsonContent));

            SetCurrentAccount(desserializedAcc);
        }

        #endregion

    }
}
