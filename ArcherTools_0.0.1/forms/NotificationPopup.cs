using static ArcherTools_0._0._1.enums.ReturnCodeEnum;

namespace ArcherTools_0._0._1.forms
{
    public partial class NotificationPopup : Form
    {
        internal Form _thisForm;
        internal Label notifTitle;
        internal Label notifDescription;
        internal PictureBox notifIcon;
        internal string notifType;
        internal ReturnCode errorCode;

        public NotificationPopup(string title, string description, string type, Image icon = null, ReturnCode error = ReturnCode.None)
        {
            notifTitle.Text = title;
            notifDescription.Text = description;
            notifType = type;
            notifIcon.Image = icon;
            errorCode = error;
            
        }
    }
}
