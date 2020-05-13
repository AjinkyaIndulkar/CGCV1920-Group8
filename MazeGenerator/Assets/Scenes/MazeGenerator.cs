using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


 enum DIRECTION : int
{
    left,
    right,
    top,
    bottom
}

public enum ORIENTATION : int
{
    NW = 0,
    NE,
    SE,
    SW
}

public class WALLS
{
    public bool T;
    public bool L;
    public bool R;
    public bool B;

    public WALLS()
    {
        T = true;
        B = true;
        L = true;
        R = true;
    }
}

public class MazeGen
{
    public int algorithm;
    public int sx;
    public int sy;
    
    public MazeGen(int _algorithm, int _sx=32,int _sy =32)
    {
        algorithm = _algorithm;
        sx = _sx;
        sy = _sy;

    }


    public WALLS[,] Gen_Wilsons(int w, int h, int seed, ORIENTATION o)
    {
        WALLS[,] walls = new WALLS[w, h];

        return walls;
    }

    public WALLS[,] Gen_bintree(int w, int h, int seed,ORIENTATION o)
    {
        //ORIENTATION o = ORIENTATION.SE;

        System.Random rndgen = new System.Random(seed);
        WALLS[,] walls = new WALLS[w, h];


        for (int i = 0; i < w; i++)
            for (int j = 0; j < h; j++)
            {
                walls[i, j] = new WALLS();
            }


        int XS = 0, YS = 0, XE = 0, YE = 0, XI = 0, YI = 0;


        if (o == ORIENTATION.SW)
        { 
            XS = 0;
            YS = 0;
            XE = w - 1;
            YE = h - 1;
            XI = 1;
            YI = 1;
        }

        if (o == ORIENTATION.NE)
        {
            XS = w - 1;
            YS = h - 1;
            XE = 0;
            YE = 0;
            XI = -1;
            YI = -1;
        }

        if (o == ORIENTATION.NW)
        {
            XS = 0;
            YS = h - 1;
            XE = w - 1;
            YE = 0;
            XI = 1;
            YI = -1;
        }

        if (o == ORIENTATION.SE)
        {
            XS = w - 1;
            YS = 0;
            XE = 0;
            YE = h - 1;
            XI = -1;
            YI = 1;
        }


        for (int i = XS; i != XE; i+=XI)
            for (int j = YS; j != YE; j+=YI)
            {
                var r = rndgen.Next(0, 20000) % 2;

                if (r == 0)
                {
                    if ((o == ORIENTATION.NE) || (o == ORIENTATION.NW))
                    {
                        walls[i, j].L = false; walls[i + XI, j].R = false;
                    }
                    if ((o == ORIENTATION.SW) ||  (o == ORIENTATION.SE))
                    {
                        walls[i, j].R = false; walls[i + XI, j].L = false;
                    }
                }
                if (r == 1)
                {
                    if ((o == ORIENTATION.NE) || (o == ORIENTATION.NW))
                    {
                        walls[i, j].B = false; walls[i, j + YI].T = false;
                    }
                    if ((o == ORIENTATION.SW) || (o == ORIENTATION.SE))
                    {
                        walls[i, j].T = false; walls[i, j + YI].B = false;
                    }

                }
            }

        return walls;
    
    }
    

}




public class MazeGenerator : MonoBehaviour
{

    // Start is called before the first frame update

    public InputField iField;
    public Button Ibutton;
    public Dropdown Idropdown;

    [SerializeField] public RenderTexture rt;
    [SerializeField] public Texture2D Wall;
    [SerializeField] public int n_walls_x = 32;
    [SerializeField] public int n_walls_y = 32;

    //private Renderer renderer = new Renderer(); // renderer in which you will apply changed texture
    private Texture2D texture;
    private Texture2D texturewall;
    int w = 0;
    int h = 0;
    MazeGen MZ = new MazeGen(1, 32, 32);

    Color[,] Colors_;



    System.Random rndgen;//= new System.Random();
    int seed = 0;
    int x_wall_size = 16;
    int y_wall_size = 16;


    float[,] WALLPIXELS_L;
    float[,] WALLPIXELS_R;
    float[,] WALLPIXELS_B;
    float[,] WALLPIXELS_T;
    
    void init_walls()
    {

        WALLPIXELS_L = new float[x_wall_size, y_wall_size];
        WALLPIXELS_R = new float[x_wall_size, y_wall_size];
        WALLPIXELS_B = new float[x_wall_size, y_wall_size];
        WALLPIXELS_T = new float[x_wall_size, y_wall_size];

        for (int mx = 0; mx < x_wall_size; mx++)
        {
            for (int my = 0; my < y_wall_size; my++)
            {
                if (mx < 1)
                    WALLPIXELS_L[mx, my] = 1;

                if (mx > (x_wall_size-1))
                    WALLPIXELS_R[mx, my] = 1;

                if (my < 1)
                    WALLPIXELS_B[mx, my] = 1;

                if (my > (x_wall_size -1))
                    WALLPIXELS_T[mx, my] = 1;
            }
        }

    }

    WALLS[,] walls;
    void Start()
    {
        rndgen = new System.Random(seed);

        w = rt.width;
        h = rt.height;
        
        texture = new Texture2D(w,h,TextureFormat.RGBAFloat,false);
        //texturewall = new Texture2D(32,32,TextureFormat.RGBAFloat, false);

        Colors_ = new Color[w, h];

        x_wall_size = w / n_walls_x;
        y_wall_size = w / n_walls_y;


        init_walls();

        Ibutton.onClick.AddListener(TaskOnClick);
        Idropdown.onValueChanged.AddListener(delegate { TaskOnAlgChanged(Idropdown); });

        walls = MZ.Gen_bintree(n_walls_x, n_walls_y, seed, ORIENTATION.NW);
    }

    void TaskOnAlgChanged(Dropdown change)
    {
        var txt = change.options[change.value].text;
        print(txt);
        seed = rndgen.Next(0, 10000);
        if(txt == "Binary Tree NW")
            walls = MZ.Gen_bintree(n_walls_x, n_walls_y, seed, ORIENTATION.NW);
        if (txt == "Binary Tree NE")
            walls = MZ.Gen_bintree(n_walls_x, n_walls_y, seed, ORIENTATION.NE);
        if (txt == "Binary Tree SW")
            walls = MZ.Gen_bintree(n_walls_x, n_walls_y, seed, ORIENTATION.SW);
        if (txt == "Binary Tree SE")
            walls = MZ.Gen_bintree(n_walls_x, n_walls_y, seed, ORIENTATION.SE);
    }
    void TaskOnClick()
    {
        seed = rndgen.Next(0, 10000);
        TaskOnAlgChanged(Idropdown);
    }
    // Update is called once per frame

    void Update()
    {
        

        RenderTexture.active = rt;

        for (int i = 0,I=0; i < w; i += x_wall_size,I++)
        {
            for (int j = 0,J=0; j < h; j += y_wall_size,J+=1)
            {


                for (int mx = 0; mx < x_wall_size; mx++)
                {
                    for (int my = 0; my < y_wall_size; my++)
                    {
                        float F = 0;

                        if (walls[I, J].T)
                            F += WALLPIXELS_T[mx,my];
                        if (walls[I, J].B)
                            F += WALLPIXELS_B[mx, my];
                        if (walls[I, J].L)
                            F += WALLPIXELS_L[mx, my];
                        if (walls[I, J].R)
                            F += WALLPIXELS_R[mx, my];

                        texture.SetPixel(i + mx, j + my, new Color(F,F, 0, 0));
                    }
                }
            }
        }

        texture.Apply();

        Graphics.Blit(texture, rt);

        RenderTexture.active = null;

    }

    void OnGUI()
    {
        if (iField.isFocused && iField.text != "" && Input.GetKey(KeyCode.Return))
        {
            var TX = iField.text;
            print(TX);
            n_walls_x = 64;
            n_walls_y = 64;
            Start();
            iField.text = TX;
        }

        
    }
}
