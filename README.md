comingsoonnotes2020-05-22#12h31pm - new version SCCSGimbal V1.2 coming today the 2020-05-22. upgraded "sccsaiguess.cs" that for some reasons, i thought "why would i do a random guess of 3 points and why not just 2 points maximum". Youre gonna say why? why not just 1 maybe because if you put just 1 point, the AI SHOULD be able to do the rest but i think my issue there is that it's taking a minimum of "3" length on the 
training size of the perceptron trainer.cs. I didn't investigate that issue yet more than thinking also, why not use a point for each angle in a 3d sphere "type cartesian to polar coordinates" and send that to the perceptron? right? why not a spiral 3d sphere... woaahhh. joking, Sebastian Lague, has done the 3d spiral already that you can find on his youtube channel here https://www.youtube.com/watch?v=bqtqltqcQhw so why look elsewhere for a template that works and that you can modify to your own needs. and don't look nowhere else for a 2d version of a spiral "array" for an included 2D pathfinding, ultra low weigth and very performant, completely translated from Sebastian Lague series on pathfinding here https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW(and modified a bit for Void Expanse) in my scripts at Atomic Torch here http://forums.atomictorch.com/index.php?topic=969.msg7791#msg7791 but it is a draft explanation only as the whole scripts were in the mod and downloadable July 30, 2019. i thought Sebastian Lague's was using arrays and indexing in the arrays to determine the index and location just like i am doing it but it seems completely different. i am really relieved and surprised as i don't like using Cos/Sin/Tan except at the worst case scenario when i have to use also the Pythagore stuff for Inverse Kinematics. It's annoying me to have to go back to those godamn mathsisfun https://www.mathsisfun.com/ and wolfram's circle circle
intersect https://www.google.ca/search?sxsrf=ALeKk00PW61qxlJQdXLHiGSI3lbPgRJhWA%3A1590166717447&source=hp&ei=vQTIXr6EGIKE9PwPmO-IoA8&q=wolfram+circle+circle+intersect&oq=wolfram+circl&gs_lcp=CgZwc3ktYWIQAxgAMgQIIxAnMgUIABDLATIFCAAQywEyBQgAEMsBMgYIABAWEB4yBggAEBYQHjIGCAAQFhAeMgYIABAWEB4yBggAEBYQHjIGCAAQFhAeOgIIADoFCAAQgwE6BAgAEAo6BwgjELECECc6BwgAEAoQywFQ-gFYiC9gmDNoCnAAeACAAWeIAYUPkgEEMTguM5gBAKABAaoBB2d3cy13aXo&sclient=psy-ab
I want to make a bitcoin game so that instead of people having to go die to work, that they work to live and make their governments get percentages of the bitcoins made from video game playing as in the more cpus that work, the more the players make money, the more your
government does, the more your government is able to do "The Game by Sean Penn on Michael Douglas" to you at all times 24/7. There is the light at the end of the tunnel said "Stephen Colbert" one day. Me, i just happened to realize i could do what the fuck i wanted in life and im just trying to get my "sinking life boat" pointed in the right direction and right now it seems to be heading towards a lot of anuses. "modified quote of Steve Jobs" in conference with Bill Gates here https://www.youtube.com/watch?v=wvhW8cp15tk.



personalnote2020-05-22: Worms Bitcoin Game Destructible Voxel. I will be approaching Team17 studios with a new Worms Game with Planet Destruction and machine learning. but it will also incorporate Bitcoin mining. if you approach Team17 before me for that project, i don't care because i wrote it down here on the 22nd of May 2020 and i did an update on this commit personal notes.

using Unity3D 2017.4.39f1

[img]https://i.ibb.co/Vw21X5L/sccsgimbal0.png[/img]

# SCCSGimbal v1.1
English: using Brollof's Simple Perceptron found here https://github.com/Brollof/SimplePerceptron , i have made a gimbal machine learning system for the x/y/z axis. you just need to move the "northpole/bullseye" around in the unity3D EDITOR. it's not working for the GAME mode yet.

Français: J'utilise le Perceptron Brollof Simple et linéaire que vous pouvez trouver ici https://github.com/Brollof/SimplePerceptron  et j'ai construit un systèmes de gimballes "machine learning" pour les axes x/y/z. vous avez juste besoin de déplacer le "PoleNord/bullseye" dans le Unity3D EDITOR. ça ne fonctionne pas présentement dans le Unity3D GAME mode

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
        
        
        
        
        
        
        


sc_core_systems@outlook.com
https://twitter.com/sccoresystems1
https://ninekorn.imgbb.com/
https://www.twitch.tv/sccoresystems
currently working on
https://www.youtube.com/watch?v=yWspu7zvbBU
https://forums.frontier.co.uk/threads/virtual-desktop-program-with-embedded-physics-engine-at-the-press-of-a-button-coming-in-2020.542577/
https://github.com/ninekorn/SCCSCompass




