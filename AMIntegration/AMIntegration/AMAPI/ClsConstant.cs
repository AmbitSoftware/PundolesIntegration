
namespace AMIntegration.AMAPI
{
    public class ClsConstant
    {
        public const int SuccessCode = 5;
        public const string SuccessMessage = "Success";

        public const int AuthenticationFailureCode = 10;
        public const string AuthenticationFailureMsg = "Authentication Failure";

        public const int ServerErrorCode = 30;
        public const string ServerErrorMsg = "Internal Server Error";

        public const int FieldException = 40;

        public const int InvalidrequestparametersCode = 90;
        public const string InvalidrequestparametersMessage = "Invalid request parameters.";

        public const int atmCDMMandatoryFieldsCode = 49;
        public const string atmCDMMandatoryFieldsMessage = "Device number, Device type, Card type and Mode of payment are missing for area selected ATM CDM.";

        public const int deviceTypeAndModeOfPaymentConditionCode1 = 61;
        public const string deviceTypeAndModeOfPaymentConditionMessage1 = "Please select mode of payment as Cash. Cheque is not applicable as Card type selected is Credit Card.";

        public const int deviceTypeAndModeOfPaymentConditionCode = 62;
        public const string deviceTypeAndModeOfPaymentConditionMessage = "Please select mode of payment as Cash. Cheque is not applicable for device type ATM.";

    }

    public class ClsConstantGetListofATMCDM
    {

    }

    public class ClsConstantGetListofBranches
    {

    }

    public class ClsConstantAddNewCase
    {
        public const int processIDNotExistCode = 41;
        public const string processIDNotExistMessage = "Process ID is not exist in the SUITE CRM.";

        public const int fieldsMandatoryForProcessIDCode = 42;
        public const string fieldsMandatoryForProcessIDMessage = "Fields are mandatory for the ProcessID.";

        public const int combinationNotExistForprocessIDCode = 43;
        public const string combinationNotExistForprocessIDMessage = "Combination of Area,Category,SubCategory and Issue is not exist for the ProcessID.";

        public const int accountNotExistforCifNoCode = 44;
        public const string accountNotExistforCifNoMessage = "There is no customer exist for CifNo.Invalid CifNo!";

        public const int fieldsValueNotExistforCifNoCode = 45;
        public const string fieldsValueNotExistforCifNoCodeMessage = "Combination of BankingWith,CustomerCategory and CustomerType is not exist for the Customer CifNo.";

        public const int finanAccountNotExistForCifNoCode = 46;
        public const string finanAccountNotExistForCifNoMessage = "Account No. does not exist in the Suite CRM.";

        public const int invalidCurrencyCode = 47;
        public const string invalidCurrencyCodeMessage = "Invalid Currency";

        public const int invalidDeviceNoCode = 48;
        public const string invalidDeviceNoMessage = "Invalid DeviceNo.";

        public const int invalidDeviceNoDataCombinationCode = 53;
        public const string invalidDeviceNoDataCombinationMessage = "Invalid Device type or Card type for existing customer CIFNo.";

    }

    public class ClsConstantAddNewCardDisputeCase
    {
        public const int attachmentsMissingforDisputeCode = 51;
        public const string attachmentsMissingforDisputeMessage = "Attachments are missing for Dispute issue.";

        public const int invalidDisputeCode = 52;
        public const string invalidDisputeMessage = "Invalid Dispute Issues.";
    }

    public class ClsConstantUpdateCaseDetails
    {
        public const int invalidStatusCode = 55;
        public const string invalidStatusMessage = "Invalid Status.";

        public const int invalidMOPCode = 56;
        public const string invalidMOPMessage = "Invalid mode of payment.";

        public const int deviceTypeAndCardTypeNotNatchingCode = 57;
        public const string deviceTypeAndCardTypeNotNatchingMessage = "Device Type or Card Type is not matching with the Device no.";

        public const int deviceNoDoesnotExistintoMasterCode = 58;
        public const string deviceNoDoesnotExistintoMasterMessage = "Device no. doesnot exist into the Device master table.";

        public const int areaShouldbeATMCDMFFMCode = 67;
        public const string areaShouldbeATMCDMFFMMessage = "If Device no. is there, then Area should be equal to ATM/CDM/FFM.";

        public const int linkexpiredCode = 68;
        public const string linkexpiredMessage = "Link expired.";

        public const int ratingalreadyprovidedCode = 69;
        public const string ratingalreadyprovidedMessage = "Rating already provided.";

    }

    public class ClsConstantGetCaseList
    {

    }

    public class ClsConstantGetCaseCount
    {

    }
    public class ClsConstantGetCaseStatus
    {

    }

    public class ClsConstantGetCaseDetails
    {

    }

    public class ClsConstantGetListofProcessMatrices
    {


    }

    public class ClsConstantIsSystemAvailable
    {


    }

    public class ClsConstantGetDropdownMaster
    {


    }

    public class ClsConstantGetDisputeIssue
    {


    }

    public class ClsConstantGetDisputedTransactions
    {

    }

    public class ClsConstantCreateUser
    {
        public const int userAlreadyPresentCode = 100;
        public const string userAlreadyPresentMsg = "User already Present.";
    }

    public class ClsConstantEditUser
    {
        public const int userDoesNotExitCode = 200;
        public const string userDoesNotExitMsg = "User Does Not Exist.";
    }

    public class ClsConstantGetUserStatus
    {

    }


}