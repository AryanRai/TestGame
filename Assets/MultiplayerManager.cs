
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Linq;


public class MultiplayerManager : MonoBehaviour
{
    public GameObject prefab;
    List<GameObject> playerlistobj;
    public string username;
    public GameObject player;
    private int playercount = 0;
    private int localplayercount = 0;
    private Dictionary<string, object> multiplayerdatadict;
    private Dictionary<string, object> prevmultiplayerdatadict;
    private List<string> playerlist;
    private List<string> prevplayerlist;
    public float speed = 6f;
    private List<string> animationstatus;
    private List<float> animationstatustime;
    //public string playeranimationstatus = "idle_normal";
    public string mainplayeranimation = "";
    public string mainplayeranimationspeed = "";

    
    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;
    


    // Start is called before the first frame update
    
    void Start()
    {
        playerlistobj = new List<GameObject>();
        prevmultiplayerdatadict = new Dictionary<string, object>();
        multiplayerdatadict = new Dictionary<string, object>();
        animationstatustime = new List<float>();
        animationstatus = new List<string>();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        DatabaseReference OverallUserRef = FirebaseDatabase.DefaultInstance.GetReference("players"); 
        DatabaseReference CurrentUserDataRef = FirebaseDatabase.DefaultInstance.GetReference("players").Child(username); 
        

        if (username != "") {
        OverallUserRef.ValueChanged += HandleValueChanged;
        
        }
                
    }

    void destroyplayersall(){
        if (playerlistobj.Count > 0) {
        var count = playerlistobj.Count;
        for (int i = 0; i < count; i++)
        {
        //Debug.Log("destroy");
        //Debug.Log(playerlistobj[0].name);
        Destroy(playerlistobj[0]);
        playerlistobj.RemoveAt(0);
        animationstatus.RemoveAt(0);
        animationstatustime.RemoveAt(0);
        playerlist.RemoveAt(0);     
        }
        
        Debug.Log(playerlistobj.Count);

        }
    }


    void updatemultiplayerobj(Dictionary<string, object> data){

        if (localplayercount > 0){
        if (data != null) {
        //Debug.Log("notnull");
        //Debug.Log("updating pos");
        int count = playerlist.Count();
        //Debug.Log(count);
        
        for (int i = 0; i < count; i++) {

        //Debug.Log(playerlist[i]); 
        var playerinfo = data[playerlist[i]] as Dictionary<string, object>;

        
        string[] playerpos = playerinfo["pos"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["pos"].ToString().Length -2, 1).Split('f');
        string[] playerrot = playerinfo["rot"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["rot"].ToString().Length -2, 1).Split('f');
        string[] playerscale = playerinfo["scale"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["scale"].ToString().Length -2, 1).Split('f');
        string[] playeranimspeed = playerinfo["animspeed"].ToString().Split(',');
        string playeranim = playerinfo["anim"].ToString();

        //Debug.Log(playeranimspeed[0]);
        //Debug.Log(playeranimspeed[1]);
        //Debug.Log(playeranim);

        float xpos = float.Parse(playerpos[0].Replace(',', '\0'));
        float ypos = float.Parse(playerpos[1].Replace(',', '\0'));
        float zpos = float.Parse(playerpos[2].Replace(',', '\0'));
        
        float xrot = float.Parse(playerrot[0].Replace(',', '\0'));
        float yrot = float.Parse(playerrot[1].Replace(',', '\0'));
        float zrot = float.Parse(playerrot[2].Replace(',', '\0'));
        float wrot = float.Parse(playerrot[3].Replace(',', '\0'));

        float xscale = float.Parse(playerscale[0].Replace(',', '\0'));
        float yscale = float.Parse(playerscale[1].Replace(',', '\0'));
        float zscale = float.Parse(playerscale[2].Replace(',', '\0'));

        float step =  speed * Time.deltaTime; // calculate distance to move
        Animator anim = playerlistobj[i].GetComponent<Animator> ();
        //if (playerlistobj[i].transform.position != new Vector3(xpos, ypos, zpos)){
            //if (animationstatus[i] != "running") {
         if(playeranim != "idle_normal"){
            
            //anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
            anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
            animationstatus[i] = playeranim;
            animationstatustime[i] = Time.time;
         }
            if(playeranim == "idle_normal"){
                anim.SetFloat ("Blend", 0, 0, 0);
                animationstatus[i] = "idle_normal";
            }
            //Debug.Log(Time.time);
            //Debug.Log(playerlist[i]);

            //}
        //}

        
        
        playerlistobj[i].transform.position = new Vector3(xpos, ypos, zpos);
        //playerlistobj[i].transform.position = Vector3.MoveTowards(playerlistobj[i].transform.position, new Vector3(xpos, ypos, zpos), step);
        //playerlistobj[i].GetComponent<CharacterController>()
        playerlistobj[i].transform.rotation = new Quaternion(xrot, yrot, zrot, wrot);
        playerlistobj[i].transform.localScale = new Vector3(xscale, yscale, zscale);
        
        
        //anim.SetTrigger("normal");
        
        
 

        }

        }

        }

    }

    void spawnsystematic(int count, Dictionary<string, object> data){
        
        if (data != null) {
        //Debug.Log("notnull");
        destroyplayersall();
        playerlist = data.Keys.ToList();               
        
        for (int i = 0; i < count - 1; i++) {

        //Debug.Log("spawn");
        //Debug.Log(playerlist[i]);        
        var playerinfo = data[playerlist[i]] as Dictionary<string, object>;
        
        
        //Debug.Log(playerinfo["pos"].ToString());
        //Debug.Log(playerinfo["rot"].ToString());
        //Debug.Log(playerinfo["scale"].ToString());
             
       //Debug.Log(temp_pos);

        string[] playerpos = playerinfo["pos"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["pos"].ToString().Length -2, 1).Split('f');
        string[] playerrot = playerinfo["rot"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["rot"].ToString().Length -2, 1).Split('f');
        string[] playerscale = playerinfo["scale"].ToString().Replace(' ', 'f').Remove(0,1).Remove(playerinfo["scale"].ToString().Length -2, 1).Split('f');

        float xpos = float.Parse(playerpos[0].Replace(',', '\0'));
        float ypos = float.Parse(playerpos[1].Replace(',', '\0'));
        float zpos = float.Parse(playerpos[2].Replace(',', '\0'));
        
        float xrot = float.Parse(playerrot[0].Replace(',', '\0'));
        float yrot = float.Parse(playerrot[1].Replace(',', '\0'));
        float zrot = float.Parse(playerrot[2].Replace(',', '\0'));
        float wrot = float.Parse(playerrot[3].Replace(',', '\0'));

        float xscale = float.Parse(playerscale[0].Replace(',', '\0'));
        float yscale = float.Parse(playerscale[1].Replace(',', '\0'));
        float zscale = float.Parse(playerscale[2].Replace(',', '\0'));

        //Debug.Log(xpos);
        //Debug.Log(ypos);
        //Debug.Log(zpos);

        //Debug.Log(xrot);
        //Debug.Log(yrot);
        //Debug.Log(zrot);

        playerlistobj.Add(Instantiate(prefab, new Vector3(xpos, ypos, zpos), new Quaternion(xrot, yrot, zrot, wrot)) as GameObject);
        playerlistobj[i].name = playerlist[i];
        playerlistobj[i].transform.localScale = new Vector3(xscale, yscale, zscale);
        //Debug.Log(playerlistobj[i].transform.position);
        
        animationstatus.Add("idle_normal"); 
        animationstatustime.Add(0);
        }
        
        Debug.Log("spawning/refreshing players"); 
        prevmultiplayerdatadict = multiplayerdatadict;
        //Debug.Log();
        }
        
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args){
        //Debug.Log("called");
        if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
        // Do something with the data in args.Snapshot
        //args
        playercount = int.Parse(args.Snapshot.ChildrenCount.ToString());             
        multiplayerdatadict = (args.Snapshot.Value as Dictionary<string, object>);
        multiplayerdatadict.Remove(username);
        localplayercount = playerlistobj.Count;

        if (localplayercount != playercount- 1) {
            
        //Debug.Log("bitc");

        //Debug.Log(localplayercount);
        //Debug.Log(playercount);
        spawnsystematic(playercount, multiplayerdatadict);
		

        }

        if (localplayercount == playercount - 1) {
            
        updatemultiplayerobj(multiplayerdatadict);    
            
        }

    }


    void setplayerdataonserver(){
        if (username != "") {
        DatabaseReference CurrentUserDataRef = FirebaseDatabase.DefaultInstance.GetReference("players").Child(username); 
        var pos = player.transform.position;
        var rot = player.transform.rotation;
        var scale = player.transform.localScale;
        mainplayeranimation = player.GetComponent<thirdpersonmovement>().playeranimationstatus;
        mainplayeranimationspeed = player.GetComponent<thirdpersonmovement>().playeranimationspeed;

        var coordinates = new Dictionary<string, string>(){
        {"pos", pos.ToString()},
        {"rot", rot.ToString()},
        {"scale", scale.ToString()},
        {"anim", mainplayeranimation.ToString()},
        {"animspeed", mainplayeranimationspeed.ToString()}
        };
        //var playercountref = OverallUserRef.GetValueAsync().ContinueWith(task => {
        
        CurrentUserDataRef.SetValueAsync(coordinates);
        }
    }


    // Update is called once per frame
    void Update()
    {

    DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    DatabaseReference OverallUserRef = FirebaseDatabase.DefaultInstance.GetReference("players"); 
    
    
    if (username != "") {
 
        setplayerdataonserver();
        
/*
            if (task.IsFaulted)
	        {
		    // Failure
	        }
	        else if (task.IsCompleted)
	        {
		    DataSnapshot snapshot = task.Result;
            //firebaseplayercount(int.Parse(snapshot.ChildrenCount.ToString()));           
            playercount = int.Parse(snapshot.ChildrenCount.ToString());             
            multiplayerdatadict = (snapshot.Value as Dictionary<string, object>);
            multiplayerdatadict.Remove(username);
            // Success
	        }
*/
        //});


        if (animationstatustime.Count() != 0){
        for (int i = 0; i < playerlist.Count(); i++)
            {
                //if (Time.time - animationstatustime[i] > 0.3){
                if (Time.time - animationstatustime[i] > 0.5){
                if (animationstatus[i] != "idle_normal") {
                Animator anim = playerlistobj[i].GetComponent<Animator> ();
                anim.SetFloat ("Blend", 0, 0, 0);
                animationstatus[i] = "idle_normal";
                    }
                }
            }
        }
      
        if (Input.GetKeyDown("l")) {
            for (int i = 0; i < playerlist.Count(); i++)
            {
                Animator anim = playerlistobj[i].GetComponent<Animator> ();
                anim.SetFloat ("Blend", 0, 0, 0);
                animationstatus[i] = "idle_normal";
            }
            
        }

    }

    else {
    
        Debug.LogError("pls enter a username bish");
    
    }
        


   }
}
