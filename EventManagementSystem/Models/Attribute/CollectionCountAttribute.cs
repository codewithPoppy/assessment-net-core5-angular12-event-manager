using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementSystem.Models.Attribute
{
  public class CollectionCountAttribute : ValidationAttribute
  {
    private readonly int _minCount;

    public CollectionCountAttribute(int minCount)
    {
      _minCount = minCount;
    }

    public override bool IsValid(object value)
    {
      var collection = value as ICollection;
      if (collection != null)
      {
        return collection.Count >= _minCount;
      }
      return false;
    }
  }
}
