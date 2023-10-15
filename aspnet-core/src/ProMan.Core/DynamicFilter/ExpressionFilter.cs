using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProMan.DynamicFilter
{
    public class ExpressionFilter
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public ComparisonOperator Comparison { get; set; }

        [JsonIgnore]
        public string ActualPropertyName { get; set; }
        [JsonIgnore]
        public object ActualValue { get; set; }
        [JsonIgnore]
        public Type PropertyType { get; set; }
    }

    public enum ComparisonOperator
    {
        Equal,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        NotEqual,
        Contains, //for strings  
        StartsWith, //for strings  
        EndsWith, //for strings  
        In // for list item
    }
}
