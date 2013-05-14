using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiwanLivingCareer.Core.Helper
{
    public class ResultHelper:IDisposable
    {
        private IDictionary<string, Object> _dictionary;

        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// 執行狀況
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 回傳執行後的結果物件
        /// </summary>
        public IDictionary<string, Object> DictionaryValue
        {
            get
            {
                return _dictionary;
            }
        }

        public ResultHelper()
        {
            _dictionary = null;
            this.Status = false;
        }

        public void AddDictionart(string Key, Object Value)
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, Object>();
            }
            _dictionary.Add(Key, Value);
        }

        public void RemoveDictionart(string Key)
        {
             if(_dictionary!=null)
            {
                _dictionary.Remove(Key);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dictionary != null)
                {
                    this._dictionary.Clear();
                    this._dictionary = null;
                }
            }
        }
    }
}
