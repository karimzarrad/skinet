using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class TypeListSpec:BaseSpecification<Product,string>
    {
        public TypeListSpec()
        {
                addselect(x=> x.Type);
            AddDistinct();
        }
    }
}
