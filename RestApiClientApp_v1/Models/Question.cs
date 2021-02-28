using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiClientApp_v1.Models
{
    public class Question<T>
    {
        public string Description { get; set; }
        public T ChoiceClass { get; set; }
      
    }
}
