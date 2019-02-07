using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessDapper.Attributes
{
    internal class AutoIncrement : Attribute
    {
        public readonly bool _autoIncrement;

        internal AutoIncrement(bool autoIncrement)
        {
            this._autoIncrement = autoIncrement;
        }
    }
}
