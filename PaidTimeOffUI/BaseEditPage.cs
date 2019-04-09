using System;
using System.Collections.Specialized;
using V2.PaidTimeOffBLL.Framework;

namespace PaidTimeOffUI
{
    public abstract class BaseEditPage<T> : BasePage where T : ENTBaseEO, new()
    {
        public BaseEditPage()
        {
        }

        protected virtual void LoadNew()
        {
            T baseEO = new T();
            baseEO.Init();
            LoadScreenFromObject(baseEO);
        }

        protected abstract void LoadObjectFromScreen(T baseEO);
        protected abstract void LoadScreenFromObject(T baseEO);
        protected abstract void LoadControls();
        protected abstract void GoToGridPage();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                LoadControls();
                int id = GetId();
                if (id == 0)
                {
                    LoadNew();
                }
                else
                {
                    T baseEO = new T();
                    baseEO.Load(Convert.ToInt32(id));
                    LoadScreenFromObject(baseEO);
                }
            }
        }

        public int GetId()
        {
            NameValueCollection queryString =
                DecryptQueryString(Request.QueryString.ToString());
            if (queryString == null)
            {
                return 0;
            }
            else
            {
                string id = queryString["id"];
                if ((id == null) || (id == "0"))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(id);
                }
            }
        }
    }
}