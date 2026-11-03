using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Falta colocar todas la variables posibles como private
public class ProgressDirector : MonoBehaviour
{
    public static ProgressDirector progressDirector;

    //Estado de los niveles | 1 para bloqueado, 2 para disponible, 3 para completado y 4 para perfecto
    public int[] stateLevels;

    //Estado de los niveles de prueba | 1 para bloqueado, 2 para disponible, 3 para completado y 4 para perfecto
    public int levelEx;
    public int levelExe;

    //Variables para determinar si se muestran o no los botones
    public bool level_11;
    public bool level_12;
    public bool bossRush;
    public bool survival;

    //Checkpoints de niveles | 0 si no hay checkpoint 1 si hay checkpoint
    public int check_Exe;
    public int check_3;
    public int check_6;
    public int check_9;
    public int check_11;

    // Start is called before the first frame update
    void Start()
    {
        //Codigo para mantener el script y su información cada vez que se cambia de escena
        if (progressDirector == null)
        {
            progressDirector = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Verificar si es posible que la comprobaciones solo se hagan una vez
        CheckingProgress_Levels_1_10();

        CheckingProgress_Level_11();

        CheckingProgress_Level_12();
    }

    //Función que se llama para corroborar el estado de progresión de los niveles
    private void CheckingProgress_Levels_1_10()
    {
        for(int i = 0; i < 10; i++)
        {
            if (stateLevels[i] >= 3 && stateLevels[i+1] == 1)
            {
                stateLevels[i+1]++;
            }
        }
    }

    private void CheckingProgress_Level_11()
    {
        for(int i=0; i < 10; i++)
        {
            if(stateLevels[i] != 4)
            {
                return;
            }
        }
        level_11 = true;
    }

    private void CheckingProgress_Level_12()
    {
        if(stateLevels[10] != 4)
        {
            level_12 = false;
        }
        else
        {
            level_12 = true;
        }
    }

    //Getters y Setter
    public int getStateLevels(int level) {
		return this.stateLevels[level];
	}

	public void setStateLevels(int level, int state) {
		this.stateLevels[level] = state;
	}

    public int getLevelEx() {
		return this.levelEx;
	}

	public void setLevelEx(int levelEx) {
		this.levelEx = levelEx;
	}

	public int getLevelExe() {
		return this.levelExe;
	}

	public void setLevelExe(int levelExe) {
		this.levelExe = levelExe;
	}

    public int getCheck_Exe() {
		return this.check_Exe;
	}

	public void setCheck_Exe(int check_Exe) {
		this.check_Exe = check_Exe;
	}

	public int getCheck_3() {
		return this.check_3;
	}

	public void setCheck_3(int check_3) {
		this.check_3 = check_3;
	}

	public int getCheck_6() {
		return this.check_6;
	}

	public void setCheck_6(int check_6) {
		this.check_6 = check_6;
	}

	public int getCheck_9() {
		return this.check_9;
	}

	public void setCheck_9(int check_9) {
		this.check_9 = check_9;
	}

	public int getCheck_11() {
		return this.check_11;
	}

	public void setCheck_11(int check_11) {
		this.check_11 = check_11;
	}
}
