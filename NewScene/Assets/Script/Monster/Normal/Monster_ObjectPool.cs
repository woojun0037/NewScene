using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ObjectPool : MonoBehaviour
{
    //���� ���� �������� public�� ��ġ ��Ƶ�
    public Transform _parent = null;

    [SerializeField]
    private GameObject _prefab = null;

    [SerializeField]
    private List<GameObject> _pool = null;

    float t = 0f;
    public float spawntime = 2;
    public float spwanerhp;

    public int MaxMonsterCount; //�ʱ� Ǯ���� ��� ���� ��
    public int nextIdx = 3;

    bool Spawnerdie = false;

    float killcount = 0;

    //[SerializeField]
    //      private Transform _parent = null;
    private void Awake()
    {
        //���� �� �������� ���ε��� ��ġ ���� �Ŷ� �ּ�
        //_parent = GameObject.Find("[Object Pool]").transform;
        //_prefab = Resources.Load<GameObject>("Prefab/Ball");
        //_prefab = Resources.Load<GameObject>("Sphere");

        _pool = new List<GameObject>();
    }

    private void Start()
    {
        AddPool(_prefab, MaxMonsterCount);
    }

    private void Update()
    {
        if (!Spawnerdie)
        {
            if (killcount < MaxMonsterCount)
            {
                Debug.Log("not");
            }
        }
        else
        {
            //gameObject.SetActive(false);

            //���� �� ����
            Destroy(gameObject); //this�ϸ� ��ũ��Ʈ�� ���� ����..;; �ٵ� �������� �̰� �̷����ϸ� ���� �ֵ��� ���� ��������

        }

        t += Time.deltaTime;

        if (t > spawntime)
        {
            PopObject(_prefab);
            t = 0f;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PopObject(_prefab);
        //}
    }

    /// <summary>
    /// ������Ʈ�� Pool�� ����
    /// </summary>
    /// <param name="obj">���ϴ� ������Ʈ</param>
    /// <param name="length">������ ���ϴ� ����</param>
    public void AddPool(GameObject obj, int length)
    {
        for (int i = 0; i < length; i++)
        {
            _pool.Add(Instantiate(_prefab, _parent) as GameObject);
            _pool[i].SetActive(false);
        }
    }

    /// <summary>
    /// ������Ʈ�� Pool�� ����
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);

        //��ġ �ʱ�ȭ
        // obj.transform.position = new Vector3(0f, 5f, 10f);
        //obj.transform.position = new Vector3(0, 0, 0);

        //���� ���� ��ġ �ʱ�ȭ (�� �ڵ� ������ ���Ͱ� ����� �ڸ����� �ٽ� ������)
        obj.transform.position = _parent.transform.position;

        obj.transform.SetParent(_parent);

        //Util.Log("Initialize");
    }

    /// <summary>
    /// ������Ʈ�� Pool���� üũ�� �� ������.
    /// </summary>
    /// <param name="obj">�������� �ϴ� ������</param>
    public void PopObject(GameObject obj)
    {
        GameObject gameObject = null;

        if (_parent.childCount > 0)
            gameObject = _parent.GetChild(0).gameObject;
        else
        {
            // Pool�ȿ� ������Ʈ�� ������ ������ ����� �� ����.
            //AddPool(obj, 1); //�̰Ŵ� �߰��� clone ����� �Ŷ� �ʿ�x (�� 3�� ����� �� �ʿ��Ҷ� �߰��ϸ� 4,5 .. ����
            gameObject = _parent.GetChild(0).gameObject;
        }

        gameObject.transform.SetParent(_parent.parent);
        gameObject.SetActive(true);
    }

    void MonsterDamage() //�÷��̾� ��ũ��Ʈ���� �ҷ��� + �÷��̾� ��ũ��Ʈ�� Monster_Spawn �߰�
    {
        Debug.Log("MonsterDamage()");

        if (spwanerhp > 1)
        {
            spwanerhp--;
        }
        else
        {
            Spawnerdie = true;
        }
        Debug.Log(spwanerhp);
    }

    IEnumerator spawntimes()
    {
        yield return new WaitForSeconds(spawntime);
        // isspawn = false;
    }

}