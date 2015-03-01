using UnityEngine;

namespace Assets.UDB.Scripts.ListView
{
    public interface IAdapter
    {
        /// <summary>
        /// Function that determines how a model GameObject should be transformed
        /// int a Unity UI GameObject (with a ScrollRect). Makes assumptions about which
        /// Components the model has. 
        /// </summary>
        /// <param name="model">A Unity GameObject - model</param>
        /// <returns>View</returns>
        GameObject ModelToView(Object model);
    }
}
