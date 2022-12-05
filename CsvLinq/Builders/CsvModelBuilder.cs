using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvLinq.Builders
{
    public class CsvModelBuilder
    {
        public SheetModelBuilder<T> AlternateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            name = name.Trim();
            _excelModelBuilder._sheetDictionary.Add(name.ToLower(), this);
            _sheetAlternateNames.Add(name);
            return this;
        }

        public ColumnModelBuilder<T, TReturn> Column<TReturn>(Expression<Func<T, TReturn>> memberAccessor)
        {
            if (memberAccessor.Body is MemberExpression memberExpression)
            {
                return Column(memberAccessor, memberExpression.Member.Name);
            }
            throw new ArgumentOutOfRangeException(nameof(memberAccessor), $"{nameof(memberAccessor)}.{nameof(LambdaExpression.Body)} must be a {nameof(MemberExpression)}");
        }

        public ColumnModelBuilder<T, TReturn> Column<TReturn>(Expression<Func<T, TReturn>> memberAccessor, string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (memberAccessor.Body is MemberExpression memberExpression)
            {
                var trimmedName = name.Trim();
                for (int i = 0; i < _columns.Count; i++)
                {
                    if (_columns[i].Member == memberExpression.Member && _columns[i].Name == trimmedName)
                        return (ColumnModelBuilder<T, TReturn>)_columns[i];
                }
                return new ColumnModelBuilder<T, TReturn>(this, memberExpression, name);
            }
            throw new ArgumentOutOfRangeException(nameof(memberAccessor), $"{nameof(memberAccessor)}.{nameof(LambdaExpression.Body)} must be a {nameof(MemberExpression)}");
        }

        public SheetModelBuilder<T> ReadRangeLocator(Func<ExcelWorksheet, ExcelRange> readRangeLocator)
        {
            _readRangeLocator = readRangeLocator;
            return this;
        }
    }
}
