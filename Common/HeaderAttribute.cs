using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ecommerceApi.Common
{
    public class HeaderAttribute : Attribute, IActionConstraint
    {
        private readonly string cabcera;
        private readonly string valor;

        public HeaderAttribute(string cabcera, string valor)
        {
            this.cabcera = cabcera;
            this.valor = valor;
        }

        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var cabeceras = context.RouteContext.HttpContext.Request.Headers;
            if (!cabeceras.ContainsKey(cabcera))
                return false;

            return string.Equals(cabeceras[cabcera], valor, StringComparison.OrdinalIgnoreCase);
        }
    }
}
