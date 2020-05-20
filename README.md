❮img src="images/sccsgimbal0.png" width="100" ❯

# SCCSGimbal v1.1
English: using Brollof's Simple Perceptron found here https://github.com/Brollof/SimplePerceptron , i have made a gimbal machine learning system for the x/y/z axis.

Français: J'utilise le Perceptron Brollof Simple et linéaire que vous pouvez trouver ici https://github.com/Brollof/SimplePerceptron  et j'ai construit un systèmes de gimballes "machine learning" pour les axes x/y/z.

//not available yet: other project using parts of this system:
English: A planet type "voxel minecraft" shooter game at the same time 🙂. i built this game. not available anywhere else. work in progress
Français: Un shooter de planètes type "voxel minecraft" 🙂. c'est moi qui a construit ce jeu. présentement non disponible ailleurs. en développement . 

❮img src="images/voxeldestructibleplanetaigimbal3.png" width="100" ❯
❮img src="images/voxeldestructibleplanetaigimbal.png" width="100" ❯
❮img src="images/voxeldestructibleplanetaigimbal1.png" width="100" ❯
❮img src="images/voxeldestructibleplanetaigimbal2.png" width="100" ❯

# SCCSCompass v1.0
using Brollof's Simple Perceptron found here https://github.com/Brollof/SimplePerceptron , i have made a compass. You can divide the circumference of the 2d Circle to half angles or quarter angles and so on.

1. in order to set a different waypoint/bullseye/northpole for the compass to look at, you can go in the SC_AI.cs script and 
modify or add another "else if" to the following lines to aim at another target and of course change the swtchwaypointtype to that other selectable waypoint/bullseye/northpole.

        if (swtchwaypointtype == 0)
        {
            Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);
            dirbulletprimerright.Normalize();

            Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
            dirbulletprimerforward.Normalize();
            Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
            dirprimertonorthpoletransform.Normalize();

            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

            if (_dotGoal >= 0.001f)
            {
                answer = 1;
            }
            else if (_dotGoal < -0.001f)
            {
                answer = -1;
            }
        }
