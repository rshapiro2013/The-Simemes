mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },


  RequestUserData: function () {
    if (window.unityInstance) {
      window.unityInstance.SendMessage("TelegramController", "SetWebAppUser", JSON.stringify(window.Telegram.WebApp.initDataUnsafe.user));
    }
  },


  Ready: function () {
    if (window && window.Telegram && window.Telegram.WebApp) {
      window.Telegram.WebApp.ready();
    }
  },

  Close: function () {
    if (window && window.Telegram && window.Telegram.WebApp) {
      window.Telegram.WebApp.close();
    }
  },

  Expand: function () {
    if (window && window.Telegram && window.Telegram.WebApp) {
      window.Telegram.WebApp.expand();
    }
  },






});