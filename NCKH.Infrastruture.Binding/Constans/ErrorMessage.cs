
namespace NCKH.Infrastruture.Binding.Constans
{
    public static class ErrorMessage
    {
        public const string NotHavePermission = "You do not have permission to do this action.";
        public const string SomethingWentWrong = "Something went wrong. Please contact with administrator.";
        public const string Missingsomeconfiguration = "Missing some configuration. Please contact with administrator.";
        public const string AlreadyUpdatedByAnother = "Already updated by another.";
        public const string AlreadyExists = "{0} name: {1} already exists.";
        public const string PleaseSelectStartDate = "Please select start date.";
        public const string PleaseSelectEndDate = "Please select end date.";
        public const string NotExists = "{0} does not exists.";
        public const string IsSystem = "{0} is System.";
        public const string Exists = "{0} already exists.";
        public const string CanNotBeNull = "{0} can not be null.";
        public const string MaxLength = "can not over {0} length.";
        public const string PleaseSelect = "please select {0}.";
        public const string Invalid = "{0} invalid.";
        public const string UsedBy = "{0} was used by {1}.";
        public const string CanNotChangeDefault = "You can not change default {0}.";
        public const string MustBeNumber = "{0} must be number";
        public const string Notaccepted = "{0} not accepted";
        public const string InvalidUserName = "{0} from 3 to 20 characters.";
        public const string NotacceptedUserName = "{0} contains only characters and numbers.";
        public const string CannotDelete = "This {0} has been used for the {1}. You cannot delete {0}";
        public const string IsAlreadyTaken = "{0} is already taken.";
        public const string GreaterThanOrEqualTo = "{0} greater than or equal to {1}.";
        public const string LessThanOrEqualTo = "{0} less than or equal to {1}.";
        public const string GreaterThan = "{0} greater than {1}.";
        public const string LessThan = "{0} less than {1}.";
        public const string ListError = "The postings list has no records matching the condition.";
        public const string ListErrorInsert = "No {0} added.";
        public const string ListCopyError = "user no {0} to copy.";
        public const string MustDefine = "You have to define at least one {0}.";
        public const string CanNotChangeStatus = "{0} has been {1}. You can not {2} this {0}.";
        public const string EnterReason = "Please enter decline reason.";
        public const string ErrorEnum = "Please enter value {0} within limit.";
        public const string DeleteList = "List have one value. You can't delete this value.";
        public const string PayTotal = "You need to pay by the total amount.";
        public const string MatchingError = "{0} not matching with {1}.";
        public const string CannotAction = "This {0} was paid. You can not {1}.";
        public const string AddError = "Add new failed. Please check again.";
        public const string RemoveUserBranch = "User must be joined to the branch. You can not remove this user from the branch.";
        public const string NotUpdate = "Nothing update. Please check again.";
        public const string Already3Exists = "{0} {1}: {2} already exists.";
    }

    public static class SuccessMessage
    {
        public const string AddSuccessful = "Add new {0} successful.";
        public const string UpdateSuccessful = "Update {0} successful.";
        public const string DeleteSuccessful = "Delete {0} successful.";
        public static string SyncDataSuccessful = "Sync data successful.";
        public const string CopySuccessful = "Copy {0} successful.";
        public const string SentEmailSuccessful = "Sent mail successful.";
        public const string IsAvailable = "{0} is available";
        public const string IsSuccessful = "{0} successful.";
    }
}
