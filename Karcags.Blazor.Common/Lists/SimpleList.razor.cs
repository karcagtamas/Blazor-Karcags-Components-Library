using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Karcags.Blazor.Common.Lists
{
    public partial class SimpleList<T> where T : class
    {
        [Parameter]
        public IEnumerable<T> ListElements { get; set; } = new List<T>();

        [Parameter]
        public Func<T, string> CustomDisplayer { get; set; } = (T t) => t.ToString();

        [Parameter]
        public bool Selectable { get; set; } = false;

        [Parameter]
        public bool IsMultipleSelectable { get; set; } = false;

        [Parameter]
        public IEnumerable<T> SelectedElements { get; set; } = new List<T>();

        [Parameter]
        public IEnumerable<T> DisabledElements { get; set; } = new List<T>();

        [Parameter]
        public EventCallback<IEnumerable<T>> SelectionChanged { get; set; }

        private async void Select(T element)
        {
            if (this.Selectable)
            {
                if (!this.DisabledElements.Contains(element))
                {
                    if (this.IsMultipleSelectable)
                    {
                        var selected = this.SelectedElements.ToList();
                        if (this.SelectedElements.Contains(element))
                        {
                            selected.Remove(element);
                        }
                        else
                        {
                            selected.Add(element);
                        }
                        this.SelectedElements = selected;
                    }
                    else
                    {
                        this.SelectedElements = new List<T> { element };
                    }
                    await this.SelectionChanged.InvokeAsync(this.SelectedElements);
                }
            }
        }
    }
}