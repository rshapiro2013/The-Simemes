
/**
* This object contains the data of the Mini App user.
* @see https://core.telegram.org/bots/webapps#webappuser
**/
[System.Serializable]
public class WebAppUser
{
   public long id;
   public bool is_bot;
   public string first_name;
   public string last_name;
   public string username;
   public string language_code;
   public bool is_premium;
   public bool added_to_attachment_menu;
   public bool allows_write_to_pm;
   public string photo_url;
}