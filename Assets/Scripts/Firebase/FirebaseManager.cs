using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public TMP_Text username    ;

    [Header("LoadinScreen")]
    public GameObject loadingScreen;
    public TMP_Text loadingText;
    public TMP_Text errorText;
    
    public Button retryBtn;

    GameObject ItemTemplate;
    [SerializeField] GameObject RankingContent;
    [SerializeField] Transform RankScrollView;
    static public bool once_call;

  

    private void Start()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {
           
            Destroy(gameObject);
        }

        StartCoroutine(CheckAndFixDependencies());
    }
    private IEnumerator CheckAndFixDependencies()
    {
        var Check = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(predicate: () => Check.IsCompleted);

        var dependancyResult = Check.Result;

        if (dependancyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
            yield return new WaitForEndOfFrame();
            StartCoroutine(CheckAutoLogin());
        }
        else
        {
            Debug.Log("seminternet");
        }
        
    }


    private void StartAnonimous()
    {

        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            FirebaseDatabase.DefaultInstance.GetReference("Player Status").Child(newUser.UserId).SetRawJsonValueAsync(JsonConvert.SerializeObject(new PlayerStatus().Initialize()));
            MenuManager.instance.AccountCreations();
            
        });
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
       // StartCoroutine(CheckAutoLogin());
    }



    private IEnumerator CheckAutoLogin()
    {
     
        if(user != null)
        {
            var reloadTask = user.ReloadAsync();

            yield return new WaitUntil(predicate: () => reloadTask.IsCompleted);

            AutoLogin();
            Debug.Log("AutoLogin");
        }
        else
        {
            StartAnonimous();
            Debug.Log("Criando usuario anonimo");
        }
    }

    private void AutoLogin()
    {
        if(user != null)
        {
            StartCoroutine(LoadUserData(ScreenUp));
            
        }
        else
        {
            StartAnonimous();
        }
    }

    public void ScreenUp()
    {
        FindObjectOfType<NewMenu>().ScreenUpdate();
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    //void OnDestroy()
    //{
    //    auth.StateChanged -= AuthStateChanged;
    //    auth = null;
    //}

    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    //Function for the sign out button
    public void SignOutButton()
    {
        auth.SignOut();
        MenuManager.instance.SettingsScreen.SetActive(false);

        //Criar usuario anonimo:
        StartAnonimous();


        ClearRegisterFeilds();
        ClearLoginFeilds();
    }
    //Function for the save button
    public void SaveDataButton()
    {
        StartCoroutine(UpdateCoinLevel(MenuManager.instance.LevelCoin));
        StartCoroutine(UpdateCoin(MenuManager.instance.TotalCoins));
        StartCoroutine(UpdateCash(MenuManager.instance.Cash));
        StartCoroutine(UpdateGasLevel(MenuManager.instance.LevelGas));
        StartCoroutine(UpdateMagnetLevel(MenuManager.instance.LevelMagnet));
        StartCoroutine(UpdateDashLevel(MenuManager.instance.LevelTurbo));
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<NewMenu>().ScreenUpdate();
        }
    }
    //Function for the scoreboard button
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            yield return new WaitForSeconds(2);

            username.text = user.DisplayName;
           
            
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();
            StartCoroutine(LoadUserData(ScreenUp));

        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            Credential credential = EmailAuthProvider.GetCredential(_email, _password);

            var RegisterTask = auth.CurrentUser.LinkWithCredentialAsync(credential);

            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                // User has now been created
                //Now get the result
                user = RegisterTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        warningRegisterText.text = "";
                        ClearRegisterFeilds();
                        ClearLoginFeilds();
                        StartCoroutine(LoadUserData(ScreenUp));
                        //FirebaseDatabase.DefaultInstance.GetReference("Player Status").Child(user.UserId).SetRawJsonValueAsync(JsonConvert.SerializeObject(new PlayerStatus().Initialize()));
                    }
                }

                //var RegisterTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

                //    //CreateUserWithEmailAndPasswordAsync(_email, _password);
                ////Wait until the task completes
                //yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            }

        }
    }


    public  IEnumerator UpdateSkinList(int _skinIndex)
    {
        MenuManager.instance.PlayerSkins.Add(_skinIndex);
       // JsonConvert.

        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("PlayerSkins").SetValueAsync(MenuManager.instance.PlayerSkins);
        //
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = user.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    public IEnumerator UpdateSelectSkinName(string _skinName)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("SkinNameSelected").SetValueAsync(_skinName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateCash(int _cash)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("Cash").SetValueAsync(_cash);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }
    private IEnumerator UpdateCoin(int _coins)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("Coins").SetValueAsync(_coins);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }
    private IEnumerator UpdateCoinLevel(int _coinLevel)
    {       
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("CoinUpgrade").SetValueAsync(_coinLevel);
        //
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }
    private IEnumerator UpdateGasLevel(int _gasLevel)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("GasUpgrade").SetValueAsync(_gasLevel);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }
    private IEnumerator UpdateDashLevel(int _dashLevel)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("DashUpgrade").SetValueAsync(_dashLevel);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }
    private IEnumerator UpdateMagnetLevel(int _magnetLevel)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).Child("MagnetUpgrade").SetValueAsync(_magnetLevel);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coins is now updated
        }
    }


    public void LoadFromAnotherScript()
    {
        StartCoroutine(LoadUserData(ScreenUp));
    }

    public IEnumerator LoadUserData(Action callback)
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("Player Status").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.LogWarning(message: $"DB null");
        }
        else
        {
            
             //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            
            

            MenuManager.instance.SetUserName(snapshot.Child("Username").Value.ToString());
            MenuManager.instance.SetCoin(snapshot.Child("Coins").Value.ToString());
            MenuManager.instance.SetCash(snapshot.Child("Cash").Value.ToString());
            MenuManager.instance.SetHighscore(snapshot.Child("Highscore").Value.ToString());
            MenuManager.instance.SetCoinLevel(snapshot.Child("CoinUpgrade").Value.ToString());
            MenuManager.instance.SetGasLevel(snapshot.Child("GasUpgrade").Value.ToString());
            MenuManager.instance.SetMagnetLevel(snapshot.Child("MagnetUpgrade").Value.ToString());
            MenuManager.instance.SetTurboLevel(snapshot.Child("DashUpgrade").Value.ToString());
            MenuManager.instance.SetSkinName(snapshot.Child("SkinNameSelected").Value.ToString());
            MenuManager.instance.UpdateSkinList(snapshot.Child("PlayerSkins")) ;

            FindObjectOfType<NewMenu>().ScreenUpdate();

            if (callback != null)
            {
                callback();
            }
        }
    }

  
    public IEnumerator LoadScoreboardData()
    {
        //Get all the Player Status data ordered by kills amount
        var DBTask = DBreference.Child("Player Status").OrderByChild("Highscore").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            ItemTemplate = RankingContent;
            //Destroy any existing scoreboard elements
            foreach (Transform child in RankScrollView.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every Player Status UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>().Take(1))
            {
                string username = childSnapshot.Child("Username").Value.ToString();
                int highscore = int.Parse(childSnapshot.Child("Highscore").Value.ToString());
                int avatarid = 1;//int.Parse(childSnapshot.Child("Avatarid").Value.ToString());
               

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(ItemTemplate, RankScrollView);
                //if (childSnapshot.First() == item)
                //    item.firstStuff();

                //else if (childSnapshot.Last() == item)
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(1,username, highscore,avatarid);
            }

            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>().Skip(1).Take(1))
            {
                string username = childSnapshot.Child("Username").Value.ToString();
                int highscore = int.Parse(childSnapshot.Child("Highscore").Value.ToString());
                int avatarid = 1;//int.Parse(childSnapshot.Child("Avatarid").Value.ToString());


                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(ItemTemplate, RankScrollView);
                //if (childSnapshot.First() == item)
                //    item.firstStuff();

                //else if (childSnapshot.Last() == item)
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(2, username, highscore, avatarid);
            }
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>().Skip(2).Take(1))
            {
                string username = childSnapshot.Child("Username").Value.ToString();
                int highscore = int.Parse(childSnapshot.Child("Highscore").Value.ToString());
                int avatarid = 1;//int.Parse(childSnapshot.Child("Avatarid").Value.ToString());


                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(ItemTemplate, RankScrollView);
                //if (childSnapshot.First() == item)
                //    item.firstStuff();

                //else if (childSnapshot.Last() == item)
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(3, username, highscore, avatarid);
            }
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>().Skip(3).Take(17))
            {
                string username = childSnapshot.Child("Username").Value.ToString();
                int highscore = int.Parse(childSnapshot.Child("Highscore").Value.ToString());
                int avatarid = 1;//int.Parse(childSnapshot.Child("Avatarid").Value.ToString());


                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(ItemTemplate, RankScrollView);
                //if (childSnapshot.First() == item)
                //    item.firstStuff();

                //else if (childSnapshot.Last() == item)
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(5, username, highscore, avatarid);
            }

            //Go to scoareboard screen
            //UIManager.instance.ScoreboardScreen();
        }
    }


    public bool IsAnonimo()
    {
        if(auth.CurrentUser.IsAnonymous)
        {
            
            return true;         
        }
        return false;
    }

    public string UserName()
    {
        if (user.DisplayName != "")
        {
            return user.DisplayName;
        }
        else
        {
            return "Guest";
        }
    }

}