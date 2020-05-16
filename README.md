# SCCSCompass
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
