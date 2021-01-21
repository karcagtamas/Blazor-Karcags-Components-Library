using System;
using System.Collections.Generic;
using Karcags.Blazor.Common.Enums;
using Karcags.Blazor.Common.Models.Interfaces;

namespace Karcags.Blazor.Common.Models
{
    public class TableHeaderData<TList> where TList : IIdentified
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public Func<object, string> Displaying { get; set; }
        public bool IsSortable { get; set; }
        public bool IsFilterable { get; set; }
        public string FooterData { get; set; }
        public Func<List<TList>, string> FooterRunnableData { get; set; }
        public Alignment HeaderAlignment { get; set; }

        public TableHeaderData(string propertyName, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = propertyName;
            this.Displaying = x => (string)x;
            this.IsSortable = false;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, bool isSortable, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = propertyName;
            this.Displaying = x => (string)x;
            this.IsSortable = isSortable;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, bool isSortable, bool isFilterable, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = x => (string)x;
            this.IsSortable = isSortable;
            this.IsFilterable = isFilterable;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = x => (string)x;
            this.IsSortable = false;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, bool isSortable, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = x => (string)x;
            this.IsSortable = isSortable;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, Func<object, string> displaying, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = displaying;
            this.IsSortable = false;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, bool isSortable, Func<object, string> displaying, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = displaying;
            this.IsSortable = isSortable;
            this.IsFilterable = true;
            this.HeaderAlignment = alignment;
        }

        public TableHeaderData(string propertyName, string displayName, bool isSortable, bool isFilterable, Func<object, string> displaying, Alignment alignment)
        {
            this.PropertyName = propertyName;
            this.DisplayName = displayName;
            this.Displaying = displaying;
            this.IsSortable = isSortable;
            this.IsFilterable = isFilterable;
            this.HeaderAlignment = alignment;
        }
    }
}