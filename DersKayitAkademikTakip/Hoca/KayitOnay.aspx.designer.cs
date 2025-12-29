namespace DersKayitAkademikTakip.Hoca {
    
    public partial class KayitOnay {
        
        /// <summary>
        /// UpdatePanel - AJAX icin ana konteyner
        /// Icindeki kontroller degistiginde sadece bu alan guncellenir
        /// </summary>
        protected global::System.Web.UI.UpdatePanel upKayitlar;
        
        /// <summary>
        /// UpdateProgress - AJAX istegi sirasinda gosterilen yukleniyor mesaji
        /// </summary>
        protected global::System.Web.UI.UpdateProgress upProgress;
        
        /// <summary>
        /// Basari mesaji paneli
        /// </summary>
        protected global::System.Web.UI.WebControls.Panel SuccessPanel;
        
        /// <summary>
        /// Basari mesaji icerigi
        /// </summary>
        protected global::System.Web.UI.WebControls.Label SuccessText;
        
        /// <summary>
        /// Hata mesaji paneli
        /// </summary>
        protected global::System.Web.UI.WebControls.Panel ErrorPanel;
        
        /// <summary>
        /// Hata mesaji icerigi
        /// </summary>
        protected global::System.Web.UI.WebControls.Label ErrorText;
        
        /// <summary>
        /// Bekleyen kayit sayisini gosteren label
        /// </summary>
        protected global::System.Web.UI.WebControls.Label lblBekleyenSayisi;
        
        /// <summary>
        /// Kayitlari gosteren GridView
        /// </summary>
        protected global::System.Web.UI.WebControls.GridView gvKayitlar;
    }
}
