using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ObjectPool : MonoBehaviour
{
    //몬스터 굴은 여러개라 public로 위치 잡아둠
    public Transform _parent = null;

    [SerializeField]
    private GameObject _prefab = null;

    [SerializeField]
    private List<GameObject> _pool = null;

    float t = 0f;
    public float spawntime = 2;
    public float spwanerhp;

    public int MaxMonsterCount; //초기 풀에서 잡는 몬스터 수
    public int nextIdx = 3;

    bool Spawnerdie = false;

    float killcount = 0;

    //[SerializeField]
    //      private Transform _parent = null;
    private void Awake()
    {
        //몬스터 굴 여러개고 따로따로 위치 잡을 거라 주석
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

            //몬스터 굴 제거
            Destroy(gameObject); //this하면 스크립트만 제거 가능..;; 근데 문제점이 이거 이렇게하면 몬스터 애들이 정신 못차려서

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
    /// 오브젝트를 Pool에 생성
    /// </summary>
    /// <param name="obj">원하는 오브젝트</param>
    /// <param name="length">생성을 원하는 개수</param>
    public void AddPool(GameObject obj, int length)
    {
        for (int i = 0; i < length; i++)
        {
            _pool.Add(Instantiate(_prefab, _parent) as GameObject);
            _pool[i].SetActive(false);
        }
    }

    /// <summary>
    /// 오브젝트를 Pool로 복귀
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);

        //위치 초기화
        // obj.transform.position = new Vector3(0f, 5f, 10f);
        //obj.transform.position = new Vector3(0, 0, 0);

        //몬스터 생성 위치 초기화 (이 코드 없으면 몬스터가 사라진 자리에서 다시 생서됨)
        obj.transform.position = _parent.transform.position;

        obj.transform.SetParent(_parent);

        //Util.Log("Initialize");
    }

    /// <summary>
    /// 오브젝트를 Pool에서 체크한 뒤 꺼낸다.
    /// </summary>
    /// <param name="obj">꺼내고자 하는 프리팹</param>
    public void PopObject(GameObject obj)
    {
        GameObject gameObject = null;

        if (_parent.childCount > 0)
            gameObject = _parent.GetChild(0).gameObject;
        else
        {
            // Pool안에 오브젝트가 없으면 프리팹 재생성 후 보관.
            //AddPool(obj, 1); //이거는 추가로 clone 만드는 거라 필요x (예 3개 만들고 더 필요할때 추가하면 4,5 .. 가능
            gameObject = _parent.GetChild(0).gameObject;
        }

        gameObject.transform.SetParent(_parent.parent);
        gameObject.SetActive(true);
    }

    void MonsterDamage() //플레이어 스크립트에서 불러옴 + 플레이어 스크립트에 Monster_Spawn 추가
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