using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class CloudHandler : MonoBehaviour, IObjectRecoEventHandler
{

    public GameObject MainPlayer;
    /* public ImageTargetBehaviour behaviour;
    private CloudRecoBehaviour cloud;
    public GameObject MainPlayer;

    public void OnInitError(TargetFinder.InitState initError)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(TargetFinder targetFinder)
    {
        throw new System.NotImplementedException();
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        GameObject newImageTarget = Instantiate(behaviour.gameObject) as GameObject;
        MainPlayer = newImageTarget.transform.GetChild(0).gameObject;
        GameObject augmentation = null;
        if(augmentation!=null)
        {
            augmentation.transform.SetParent(newImageTarget.transform);
        }
        if(behaviour)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetbehaviour = (ImageTargetBehaviour)tracker.GetTargetFinder<ObjectTracker>().EnableTracking(targetSearchResult, newImageTarget);
        }
       
        



    }

    public void OnStateChanged(bool scanning)
    {
        if(scanning)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<TargetFinder>().ClearTrackables(false);
        }
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        CloudRecoBehaviour cloudReco = GetComponent<CloudRecoBehaviour>();
        if(cloudReco)
        {
            cloudReco.RegisterEventHandler(this);
        }
        cloud = cloudReco;
        MainPlayer = GameObject.Find("Player");
        Hide(MainPlayer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public ImageTargetBehaviour behaviour;
    void Hide(GameObject obj)
    {
        Renderer[] rends = obj.GetComponentsInChildren<Renderer>();
        Collider[] cols = obj.GetComponentsInChildren<Collider>();
        
        foreach(var item in rends)
        {
            item.enabled = false;
        }
        foreach(var item in cols)
        {
            item.enabled = false;
        }
    }

    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized");
    }
    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        GameObject newImageTarget = Instantiate(behaviour.gameObject) as GameObject;
        MainPlayer = newImageTarget.transform.GetChild(0).gameObject;
        GameObject augmentation = null;
        if(augmentation!=null)
        {
            augmentation.transform.SetParent(newImageTarget.transform);
        }
        if(behaviour)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, newImageTarget);
        }

        TargetFinder.CloudRecoSearchResult cloudResult = (TargetFinder.CloudRecoSearchResult)targetSearchResult;
        string URL = cloudResult.MetaData;
        MainPlayer.GetComponent<VideoPlayer>().url = URL.Trim();

        mCloudRecoBehaviour.CloudRecoEnabled = true;

    }




    // Use this for initialization 
    void Start()
    {
        // register this event handler at the cloud reco behaviour 
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterEventHandler(this);
        }
        MainPlayer = GameObject.Find("Player");
        Hide(MainPlayer);

    }

/*
    void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
            {
                // Restart TargetFinder
                mCloudRecoBehaviour.CloudRecoEnabled = true;
            }
        }
    }*/

}