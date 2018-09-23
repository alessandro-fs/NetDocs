using PagedList;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetDocs.UI.Extensions
{
    public static class UtilExtensions
    {
        public static string GetPageInfo(int totalRegistros, IPagedList Model)
        {
            try
            {
                if (totalRegistros == 0)
                    return Resources.Resource1.NenhumRegistroEncontrado;
                else if (totalRegistros == 1)
                    return  string.Format(Resources.Resource1.RegistroEncontrado1, totalRegistros.ToString(), Model.PageCount);
                else if (totalRegistros > 1 && (totalRegistros <= Model.PageSize))
                    return string.Format(Resources.Resource1.RegistroEncontrado2, totalRegistros.ToString(), Model.PageCount);
                else
                    return string.Format(Resources.Resource1.RegistroEncontrado3, totalRegistros.ToString(), Model.PageCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RouteValueDictionary GetRoutValues(string actionName, string controller, FormCollection collection)
        {
            var _dynamicRoutValues = new RouteValueDictionary();
            for (int _i = 1; _i < collection.AllKeys.Length; _i++)
                _dynamicRoutValues[collection.AllKeys[_i]] = collection[collection.AllKeys[_i]];

            return _dynamicRoutValues;
        }
    }    
}
