namespace OrderNow.Common
{
    public class Enums
    {
        public enum RoleType
        {
            Admin,
            Supervisor, 
            RestaurantAdmin,
            Waiter,
            User
        }
        public enum RequestStatus
        {
            Pending,
            Approved,
            Rejected
        }

        public enum FeatureType
        {
            Normal,
            Restaurant
        }

        public enum Control
        {
            ListOfText,
            ListOfImage,
            ListOfTextAndImage,
            Videos,
            Available,
            Time,
            ListOfAvailable,
        }
        
        public enum ControlType
        {
            None,
            Single,
            Multiple
        }

        public enum Day
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
    }
}
