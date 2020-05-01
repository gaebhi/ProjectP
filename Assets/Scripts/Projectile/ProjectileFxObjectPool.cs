using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFxObjectPool : Singleton<ProjectileFxObjectPool>
{
    private Dictionary<EProjectile, List<ProjectileFx>> m_objectPool;

    private void Awake()
    {
        m_objectPool = new Dictionary<EProjectile, List<ProjectileFx>>();
    }

    private void OnDestroy()
    {
        m_objectPool.Clear();
    }

    public ProjectileFx Pop(EProjectile _type)
    {
        ProjectileFx result = null;

        if (m_objectPool.ContainsKey(_type))
        {
            List<ProjectileFx> projectiles = m_objectPool[_type];

            result = projectiles[0];
            projectiles.RemoveAt(0);
            if (projectiles.Count < 1)
            {
                m_objectPool.Remove(_type);
            }
        }
        else
        {
            result = Resources.Load<ProjectileFx>(ConstValue.PATH_PROJECTILE + _type.EnumToStringFx());
            result = Instantiate(result);
        }

        result.transform.localPosition = Vector3.zero;
        result.gameObject.SetActive(true);

        return result;
    }

    public void Push(ProjectileFx _projectile)
    {
        if (m_objectPool.ContainsKey(_projectile.Type))
        {
            List<ProjectileFx> projectiles = m_objectPool[_projectile.Type];
            projectiles.Add(_projectile);
        }
        else
        {
            List<ProjectileFx> projectiles = new List<ProjectileFx>();
            projectiles.Add(_projectile);
            m_objectPool.Add(_projectile.Type, projectiles);
        }
        _projectile.gameObject.SetActive(false);
        _projectile.transform.localPosition = Vector3.zero;
        _projectile.transform.localScale = Vector3.one;
        _projectile.transform.SetParent(null);
    }
}
