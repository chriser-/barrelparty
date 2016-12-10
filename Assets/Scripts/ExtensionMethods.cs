using UnityEngine;
using System.Linq;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public static class ExtensionMethods
{
    public static void Show(this GameObject go)
    {
        if(go.activeSelf == false)
            go.SetActive(true);
    }
    public static void Hide(this GameObject go)
    {
        if(go.activeSelf == true)
            go.SetActive(false);
    }

    public static string ListToString<T>(this List<T> list)
    {
        return string.Join(", ", list.Select(x => x.ToString()).ToArray());
    }

    public static Vector3 GetNearPosition(this Transform transform, float? offset = null, float distance = 1f)
    {
        Vector3 near = transform.position;
        float extent = Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

        Renderer[] renderer = transform.GetComponents<Renderer>();
        if (renderer.Length > 0)
            extent = renderer.Max(r => Mathf.Max(r.bounds.extents.x, r.bounds.extents.z, extent));

        Collider[] collider = transform.GetComponents<Collider>();
        if (collider.Length > 0)
            extent = collider.Max(c => Mathf.Max(c.bounds.extents.x, c.bounds.extents.z, extent));

        MeshFilter[] mesh = transform.GetComponents<MeshFilter>();
        if(mesh.Length > 0)
            extent = mesh.Max(m => Mathf.Max(m.mesh.bounds.extents.x, m.mesh.bounds.extents.z, extent));


        if (offset != null)
            near += Quaternion.Euler(0, (float)offset, 0) * (Vector3.forward * extent * distance);
        else
            near += Quaternion.Euler(0, Random.Range(0, 360), 0) * (Vector3.forward * extent * distance);

        return near;
    }

    public static string Format(this float num)
    {
        //always 3 visible numers
        //123(m|k) or 12.3(m|k) or 1.23(m|k)
        if (num >= 100000000)
            return (num / 1000000).ToString("#,0M");

        if (num >= 10000000)
            return (num / 1000000).ToString("0.#") + "M";

        if (num >= 100000)
            return (num / 1000).ToString("#,0K");

        if (num >= 10000)
            return (num / 1000).ToString("0.#") + "K";

        return num.ToString("#,0");
    }
}