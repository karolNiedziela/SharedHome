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
            public const string Get = "{shoppingListId:int}";

            public const string GetMonthlyCost = "monthlycost";

            public const string AddShoppingListProduct = "{shoppingListId:int}/products";

            public const string PurchaseShoppingListProduct = "{shoppingListId:int}/products/{productName}/purchase";

            public const string PurchaseShoppingListProducts = "{shoppingListId:int}/products/purchase";

            public const string CancelPurchaseOfProduct = "{shoppingListId:int}/products/{productName}/cancelpurchase";

            public const string ChangePriceOfProduct = "{shoppingListId:int}/products/{productName}/changeprice";

            public const string SetIsDone = "{shoppingListId:int}/setisdone";

            public const string UpdateShoppingListProduct = "{shoppingListId:int}/products/{productName}/update";

            public const string Delete = "{shoppingListId:int}";

            public const string DeleteShoppingListProduct = "{shoppingListId:int}/products/{productName}";

            public const string DeleteManyShoppingListProducts = "{shoppingListId:int}/products";
        }

        public static class Bills
        {
            public const string Get = "{billId:int}";

            public const string GetMonthlyCost = "monthlycost";

            public const string PayForBill = "{billId:int}/pay";

            public const string ChangeBillCost = "{billId:int}/changecost";

            public const string CancelPayment = "{billId:int}/cancelpayment";

            public const string ChangeDateOfPayment = "{billId:int}/changedateofpayment";

            public const string Delete = "{billId:int}";
        }

        public static class HouseGroups
        {
            public const string RemoveMember = "{houseGroupId:int}/members/{personToRemoveId:Guid}";

            public const string HandOwnerRoleOver = "{houseGroupId:int}/handownerroleover";

            public const string Leave = "{houseGroupId:int}/leave";

            public const string Delete = "{houseGroupId:int}";
        }

        public static class Identity
        {
            public const string Register = "register";

            public const string Login = "login";

            public const string ConfirmEmail = "confirmemail";
        }

        public static class Invitations
        {
            public const string Get = "{houseGroupId:int}";

            public const string Accept = "accept";

            public const string Reject = "reject";

            public const string Delete = "{houseGroupId:int}";
        }
    }
}
