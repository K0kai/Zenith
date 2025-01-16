using static ArcherTools_0._0._1.enums.ErrorEnum;

namespace ArcherTools_0._0._1.forms
{
    public partial class NotificationPopup : Form
    {
        internal Form _thisForm;
        internal Label notifTitle;
        internal Label notifDescription;
        internal PictureBox notifIcon;
        internal string notifType;
        internal ErrorCode errorCode;

        public NotificationPopup(string title, string description, string type, Image icon = null, ErrorCode error = ErrorCode.None)
        {
            notifTitle.Text = title;
            notifDescription.Text = description;
            notifType = type;
            notifIcon.Image = icon;
            errorCode = error;
            
        }
    }
}
