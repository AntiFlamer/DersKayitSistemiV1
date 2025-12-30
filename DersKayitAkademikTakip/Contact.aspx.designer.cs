namespace DersKayitAkademikTakip
{
    public partial class Contact
    {
        /// <summary>
        /// Basari mesaji paneli
        /// </summary>
        protected global::System.Web.UI.WebControls.Panel pnlSuccess;

        /// <summary>
        /// Hata mesaji paneli
        /// </summary>
        protected global::System.Web.UI.WebControls.Panel pnlError;

        /// <summary>
        /// Hata mesaji icerigi
        /// </summary>
        protected global::System.Web.UI.WebControls.Literal litError;

        /// <summary>
        /// Form paneli
        /// </summary>
        protected global::System.Web.UI.WebControls.Panel pnlForm;

        /// <summary>
        /// Ad Soyad text kutusu
        /// </summary>
        protected global::System.Web.UI.WebControls.TextBox txtAdSoyad;

        /// <summary>
        /// Ad Soyad zorunlu alan dogrulayici
        /// </summary>
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvAdSoyad;

        /// <summary>
        /// E-posta text kutusu
        /// </summary>
        protected global::System.Web.UI.WebControls.TextBox txtEposta;

        /// <summary>
        /// E-posta zorunlu alan dogrulayici
        /// </summary>
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvEposta;

        /// <summary>
        /// E-posta format dogrulayici
        /// </summary>
        protected global::System.Web.UI.WebControls.RegularExpressionValidator revEposta;

        /// <summary>
        /// Konu secim listesi
        /// </summary>
        protected global::System.Web.UI.WebControls.DropDownList ddlKonu;

        /// <summary>
        /// Konu zorunlu alan dogrulayici
        /// </summary>
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvKonu;

        /// <summary>
        /// Mesaj text kutusu
        /// </summary>
        protected global::System.Web.UI.WebControls.TextBox txtMesaj;

        /// <summary>
        /// Mesaj zorunlu alan dogrulayici
        /// </summary>
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvMesaj;

        /// <summary>
        /// Gonder butonu
        /// </summary>
        protected global::System.Web.UI.WebControls.Button btnGonder;
    }
}
