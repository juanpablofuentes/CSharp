using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Controls.Table.Models
{
    public class MultiItemViewModel<T, TK> : IEnumerable<T>
        where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public IEnumerable<TK> SelectedIds { get; set; }

        public MultiItemViewModel()
        {
            SelectedIds = Enumerable.Empty<TK>();
        }

        public MultiItemViewModel(IEnumerable<T> source) : this()
        {
            Items = new List<T>(source);
        }

        public MultiItemViewModel(IEnumerable<TK> source) : this()
        {
            SelectedIds = new List<TK>(source);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
