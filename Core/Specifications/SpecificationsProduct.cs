using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class SpecificationsProduct:BaseSpecification<Product>
    {
        public SpecificationsProduct(ProductSpecParams specParams):base(x =>
           (specParams.Brands.Count==0 || specParams.Brands.Contains(x.Brand))&&
        (specParams.Types.Count==0 || specParams.Types.Contains(x.Type))&& 
        string.IsNullOrEmpty(specParams.Search)||x.Name.ToLower().Contains(specParams.Search))
        {
            ApplyPaging(specParams.PageSize*(specParams.PageIndex - 1), specParams.PageSize);
            switch (specParams.Sort)
            {
                case "priceAsc":
                    Addorderby(x => x.Price);
                    break;
                case "priceDesc":AddorderbyDescinding(x => x.Price); break;
                default:Addorderby(x => x.Name); break;
            }
        }
    }
}
