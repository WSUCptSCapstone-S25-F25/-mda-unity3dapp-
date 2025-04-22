1. Download and Installation of Altium

I successfully downloaded and installed Altium on my system. This was the first step in integrating Altium with Unity for our project.

![image](https://github.com/user-attachments/assets/90fcc915-b258-4815-908d-954fa0f9590c)

2. Configuration and Setup of Altium

After installation, I configured Altium by setting up libraries, project directories.

3. Loading an Example Project

To test the installation and configuration, I loaded an example PCB project in Altium. This helped confirm that the software was functioning correctly and provided a baseline for exporting models.

![image](https://github.com/user-attachments/assets/9da5566a-c066-4033-9f40-75f6e320a81d)

4. Researching STEP File Format & Exporting Altium Project

Before exporting, I researched the STEP file format to ensure compatibility with our workflow. Successfully exporting the project as a STEP file was a critical milestone.

![image](https://github.com/user-attachments/assets/fd9c93df-9e63-4554-aed1-88af696f5689)

5. Converting STEP to .obj Format

I converted the exported STEP file to the .obj format using an online tool. This step ensured that the model could be loaded into Unity.

![image](https://github.com/user-attachments/assets/e2cb77b1-8ee4-4410-938f-0c14c0abb3ff)

6. Loading .obj File in a Separate Unity Prototype Project

To verify the conversion, I loaded the .obj file in a separate Unity prototype project. This helped identify any potential compatibility issues before integrating it into the main project.

![image](https://github.com/user-attachments/assets/2fce52bf-7fc4-4e27-87b8-76308548fce3)

7. Loading .mtl File with .obj in Separate Unity Prototype Project

I ensured that the .mtl file loaded correctly alongside the .obj file in the prototype project. This step verified that material properties were retained during the import process.

![image](https://github.com/user-attachments/assets/8f1fbcc3-47ae-443b-a55c-ed69887de80d)

8. Final Integration: .mtl and .obj Loaded in Main Project

The final step was to successfully load both the .obj and .mtl files into the main Unity project. This marks a significant achievement in the integration process.

![image](https://github.com/user-attachments/assets/b5828e2f-9805-4dfe-ab0a-5fb45fa1b348)

9. Make final integration of files compatible with current features

The components can be selected as conductive and properly function as the test PCB did in the project.

![image](https://github.com/user-attachments/assets/118490e8-616a-43c2-934e-87411c69615d)

10. The conductive state is remembered, reads file and marks things as conductive.
    ![image](https://github.com/user-attachments/assets/bdfed200-58cd-47d6-977d-6f2649060922)
needs work on auto-import conductive materials

