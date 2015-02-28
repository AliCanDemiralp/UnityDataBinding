using UnityEngine;

namespace Assets.UDB.ListView
{
    public interface IAdapter
    {
        /// <summary>
        /// Function that determines how a model GameObject should be transformed
        /// int a Unity UI GameObject (with a ScrollRect). Makes assumptions about which
        /// Components the model has. 
        /// </summary>
        /// <param name="model">A Unity GameObject</param>
        /// <returns></returns>
        GameObject ModelToView(GameObject model);
    }
}
