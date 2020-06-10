
using Unity3D 2017.4.39f1

[img]https://i.ibb.co/Vw21X5L/sccsgimbal0.png[/img]

# SCCSGimbal v1.1
English: using Brollof's Simple Perceptron found here https://github.com/Brollof/SimplePerceptron , i have made a "three-gimbal mechanism" https://en.wikipedia.org/wiki/Gimbal_lock#/media/File:Gimbal_3_axes_rotation.gif machine learning system for the x/y/z axis. you just need to move the "northpole/bullseye" around in the unity3D EDITOR. it's not working for the GAME mode yet.

Français: J'utilise le Perceptron Brollof Simple et linéaire que vous pouvez trouver ici https://github.com/Brollof/SimplePerceptron  et j'ai construit un systèmes de "three-gimbal mechanism" https://en.wikipedia.org/wiki/Gimbal_lock#/media/File:Gimbal_3_axes_rotation.gif "machine learning" pour les axes x/y/z. vous avez juste besoin de déplacer le "PoleNord/bullseye" dans le Unity3D EDITOR. ça ne fonctionne pas présentement dans le Unity3D GAME mode

current known issues: the dot product by itself to slow down high speed rotating gimbal needles is not enough. i will soon lerp this or use both dot product and lerp. 

//not available yet: other project using parts of this system:
English: A planet type "voxel minecraft" shooter game at the same time 🙂. i built this game. not available anywhere else. work in progress
Français: Un shooter de planètes type "voxel minecraft" 🙂. c'est moi qui a construit ce jeu. présentement non disponible ailleurs. en développement . 

[img]https://i.ibb.co/6H7r1jn/voxeldestructibleplanetaigimbal.png[/img]
[img]https://i.ibb.co/jyrvbsG/voxeldestructibleplanetaigimbal1.png[/img]
[img]https://i.ibb.co/Jt6Z5Rb/voxeldestructibleplanetaigimbal2.png[/img]
[img]https://i.ibb.co/MPvm30g/voxeldestructibleplanetaigimbal3.png[/img]

        //![SCCSGimbal](/Assets/images/sccsgimbal0.png)
        //![SCCSGame](/Assets/images/voxeldestructibleplanetaigimbal3.png)
        //![SCCSGame](/Assets/images/voxeldestructibleplanetaigimbal.png)
        //![SCCSGame](/Assets/images/voxeldestructibleplanetaigimbal1.png)
        //![SCCSGame](/Assets/images/voxeldestructibleplanetaigimbal2.png)

# SCCSCompass v1.0
using Brollof's Simple Perceptron found here https://github.com/Brollof/SimplePerceptron , i have made a compass. You can divide the circumference of the 2d Circle to half angles or quarter angles and so on.

1. in order to set a different waypoint/bullseye/northpole for the compass to look at, you can go in the SC_AI.cs script and 
modify or add another "else if" to the following lines to aim at another target and of course change the swtchwaypointtype to that other selectable waypoint/bullseye/northpole.

        if (swtchwaypointtype == 0)
        {
            Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);
            dirbulletprimerright.Normalize();
            
            Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
            dirprimertonorthpoletransform.Normalize();

            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

            if (_dotGoal >= 0.001f) // maybe "x < 0.0f" here instead. will test later.
            {
                answer = 1;
            }
            else if (_dotGoal < -0.001f) // maybe "x < 0.0f" here instead. will test later.
            {
                answer = -1;
            }
        }
        
        
        
        
        
        
        

https://www.twitch.tv/sccoresystems
https://twitter.com/sccoresystems1
https://ninekorn.imgbb.com/
https://forums.frontier.co.uk/threads/virtual-desktop-program-with-embedded-physics-engine-at-the-press-of-a-button-coming-in-2020.542577/
https://github.com/ninekorn/SCCSCompass




