using System.Web.Mvc;

namespace BetBolao.Compartilhado.Models
{
    public static class HelpersCustomizados
    {
        public static string Submit(this HtmlHelper helper, string text, int id)
        {
            return string.Format("<input type=\"Submit\" value=\"{0}\"/>",text, id);
        }
    }
}
