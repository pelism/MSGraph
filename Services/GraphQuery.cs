using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pelism.MSGraph.Services
{
    public class GraphQuery : IEnumerable {
        private GraphQueryParam[] _graphQueryParams;

        public GraphQuery(GraphQueryParam[] graphQueryParam) {
            _graphQueryParams = new GraphQueryParam[graphQueryParam.Length];

            for (int i = 0; i < graphQueryParam.Length; i++) {
                _graphQueryParams[i] = graphQueryParam[i];
            }
        }

        public GraphQueryEnumerator GetEnumerator() {
            return new GraphQueryEnumerator(_graphQueryParams);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator) GetEnumerator();
        }
    }

    public class GraphQueryEnumerator : IEnumerator
    {
        public GraphQueryParam[] _graphQueryParams;

        int position = -1;

        public GraphQueryEnumerator(GraphQueryParam[] graphQueryParams)
        {
            _graphQueryParams = graphQueryParams;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _graphQueryParams.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public GraphQueryParam Current
        {
            get
            {
                try
                {
                    return _graphQueryParams[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    public class GraphQueryParam {

        public enum QueryType {
            Select,
            Expand,
            OrderBy,
            Filter,
            Top,
            Skip,
            SkipToken,
            Count
        }

        public GraphQueryParam() { }

        public GraphQueryParam(QueryType queryName, string queryValue) {
            QueryName = queryName;
            QueryValue = queryValue;
        }

        public QueryType QueryName { get; set; }

        public string QueryValue { get; set; }

        public string Query() {
            return string.Format("${0}={1}", QueryName.ToString().ToLower(), HttpUtility.UrlEncode(QueryValue));
        }
    }
}