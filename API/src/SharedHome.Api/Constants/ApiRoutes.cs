namespace SharedHome.Api.Constants
{
    public static class ApiRoutes
    {
        public static class Errors
        {
            public const string Error = "/error";

            public const string ErrorDevelopment = "/error-development";
        }

        public static class ShoppingLists
        {
            public const string Get = "{shoppingListId}";

            public const string GetMonthlyCost = "monthlycost";

            public const string AddShoppingListProduct = "{shoppingListId}/products";

            public const string PurchaseShoppingListProduct = "{shoppingListId}/products/{productName}/purchase";

            public const string PurchaseShoppingListProducts = "{shoppingListId}/products/purchase";

            public const string CancelPurchaseOfProduct = "{shoppingListId}/products/{productName}/cancelpurchase";

            public const string ChangePriceOfProduct = "{shoppingListId}/products/{productName}/changeprice";

            public const string SetIsDone = "{shoppingListId}/setisdone";

            public const string UpdateShoppingListProduct = "{shoppingListId}/products/{productName}/update";

            public const string Delete = "{shoppingListId}";

            public const string DeleteShoppingListProduct = "{shoppingListId}/products/{productName}";

            public const string DeleteManyShoppingListProducts = "{shoppingListId}/products";
        }

        public static class Bills
        {
            public const string Get = "{billId}";

            public const string GetMonthlyCost = "monthlycost";

            public const string PayForBill = "{billId}/pay";

            public const string ChangeBillCost = "{billId}/changecost";

            public const string CancelPayment = "{billId}/cancelpayment";

            public const string ChangeDateOfPayment = "{billId}/changedateofpayment";

            public const string Delete = "{billId}";
        }

        public static class HouseGroups
        {
            public const string RemoveMember = "{houseGroupId}/members/{personToRemoveId:Guid}";

            public const string HandOwnerRoleOver = "{houseGroupId}/handownerroleover";

            public const string Leave = "{houseGroupId}/leave";

            public const string Delete = "{houseGroupId}";
        }

        public static class Identity
        {
            public const string Register = "register";

            public const string Login = "login";

            public const string ConfirmEmail = "confirmemail";

            public const string ForgotPassword = "forgotpassword";

            public const string ResetPassword = "resetpassword";

            public const string ChangePassword = "changepassword";

            public const string UploadProfileImage = "uploadprofileimage";

            public const string GetProfileImage = "getprofileimage";
        }

        public static class Invitations
        {
            public const string Get = "{houseGroupId}";

            public const string Accept = "accept";

            public const string Reject = "reject";

            public const string Delete = "{houseGroupId}";
        }

        public static class Notications
        {
            public const string MarkAsRead = "markasread";
        }
    }
}
