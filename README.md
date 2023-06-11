# Control Interfaces in Virtual Reality - Thesis Project

Welcome to the official documentation for the Control Interface Manager (CIM). This documentation serves as a comprehensive guide to understanding and utilizing the Control Interface Manager, a powerful tool designed to streamline the setup process for virtual reality experiments and enhance usability for researchers.

## Introduction

Virtual reality (VR) research requires a seamless and efficient interface for controlling and managing various aspects of experiments. The Control Interface Manager offers a user-friendly solution that empowers researchers to easily configure and control virtual reality environments, enabling them to focus on their experiments rather than grappling with technical complexities.

This documentation provides an in-depth overview of the Control Interface Manager, including its features, installation instructions, usage guidelines, and customization options. Whether you are a researcher, a developer, or an enthusiast in the field of virtual reality, this documentation will equip you with the knowledge and resources to effectively utilize the Control Interface Manager in your VR experiments.

## Key Features

The Control Interface Manager offers a range of powerful features designed to enhance usability and simplify the setup process for virtual reality experiments. Some of the key features include:

### Intuitive Interface: 
The Control Interface Manager provides an intuitive and user-friendly interface, allowing researchers to easily configure and manage virtual reality environments without the need for extensive technical knowledge.

### Customizability: 
Researchers can customize the Control Interface Manager to suit their specific experimental requirements. The system offers flexible configuration options, enabling users to define and control various aspects such as input devices, interaction modes, and experimental parameters.

### Real-time Control: 
The Control Interface Manager provides real-time control over control interfaces used and configurations, allowing researchers to make on-the-fly adjustments during experiments. This feature enables seamless interaction and empowers researchers to iterate and refine their experiments efficiently.

### Extensibility: 
The Control Interface Manager is designed to be extensible, allowing developers to integrate additional functionalities and expand its capabilities. This extensibility enables researchers to adapt the system to their evolving needs and leverage emerging technologies in virtual reality research.

## Getting Started

To get started with the Control Interface Manager, refer to the installation instructions provided in the following section. This will guide you through the process of setting up the Control Interface Manager in your virtual reality environment. Once installed, you can explore the various functionalities and configurations available to customize the system according to your requirements.

I hope that this documentation will serve as a valuable resource, empowering you to harness the full potential of the Control Interface Manager in your virtual reality experiments. Should you have any questions or encounter any issues, please refer to the relevant sections or submit a ticket describing the problem.

To start using the Control Interface Manager, follow these steps:

1. *Clone the repository or download the project*: Clone the Control Interface Manager repository to your local machine using Git, or alternatively, download the project as a zip file and extract it to a directory of your choice.

2. *Download Unity LTS version 2020.3.17f1*: The Control Interface Manager is developed using Unity, and it requires the LTS (Long-Term Support) version 2020.3.17f1. Ensure that you have this version of Unity installed on your machine before proceeding.

3. *Start up the project*: Open Unity and navigate to the Control Interface Manager project folder. Once inside the project, you will find the Asset folder. Inside the Asset folder, locate the Scene folder, which contains a working deployment of the Control Interface Manager called 'CIM Testing Scene'. This scene allows you to interact with the Control Interface Manager and explore its functionalities. Please note that to fully experience the Control Interface Manager, you will need a VR Head-Mounted Display (HMD) and VR controllers.

4. *Configure input actions*: The Asset folder also contains the InputActionAsset folder. Inside this folder, you will find the file 'InputActionsCIM.inputactions'. This file defines the actions and configurations that are displayed in the Control Interface Manager within the 'CIM Testing Scene'. You can modify the input mappings and names in this file to observe how the Control Interface Manager's user interface dynamically updates its content based on the changes you make.

By following these steps, you will have the Control Interface Manager project set up on your local machine and be ready to explore its features and capabilities. The 'CIM Testing Scene' provides a hands-on experience of the Control Interface Manager's functionalities, allowing you to interact with the interface and customize it according to your needs.

## User Interface

The Control Interface Manager provides an intuitive user interface (UI) that allows you to interact with the actions and configurations defined in the InputActionsCIM file. To access the UI, follow these steps:

Once you are in the VR scene, press the left Menu button on your VR controllers to open the user interface. The UI will appear in front of you, ready for interaction.

The UI is organized into dropdown containers, each representing one of the Actions defined in the InputActionsCIM file. These dropdown containers provide a clear visual representation of the available actions. See the image below for reference:

![UI showing Action dropdown containers](https://i.ibb.co/g636F8p/Closed-Tabs.png)

To explore the details of a specific action, click on one of the 'Open' buttons associated with the action you want to inspect. Clicking the 'Open' button expands the container and reveals the information for the selected action.

![UI showing Action and Configuration details](https://i.ibb.co/TKLswV1/Locomotion-Gamepad-Connected.png)

Directly below the action name, you will find a list of tabs. Each tab represents a configuration associated with the action. By default, the 'Default' configuration is selected. The tabs allow you to navigate between different configurations and explore their mapping details and device requirements.

![UI showing Disconnected Device Status](https://i.ibb.co/6t3bXby/Locomotion-Gamepad-Disconnected.png)

As you examine the configurations, you will notice that the device boxes provide real-time status updates. If a device appears as 'Disconnected', please ensure that it is properly connected. If necessary, check the device connections or perform any required external setup. Once the device is correctly detected by the Control Interface Manager, the label will turn green and display 'Connected'.

To switch between configurations, select a different tab corresponding to the desired configuration. For example, you can switch to the Gamepad Configuration by clicking on its tab. To confirm the configuration change, press the 'Select' button located in the top right corner of the action container, next to the 'Close' button. If the configuration change is successful, the 'Select' button will be replaced with a green 'Active' label.

![UI showing Active Configuration](https://i.ibb.co/TkstqPk/Interaction-Gamepad-Active.png)

## Actions Breakdown

The Control Interface Manager is based on the concept of Actions, which are activities the user is allowed to do via the use of control interfaces. Three actions are supported in the VR environment: VRMode, Locomotion, and Interaction. These Actions are groups of input data that scripts can use as a generic interface for integrating those interactions in the virtual environment. The following paragraphs provide a description of the supported Actions as proof of concept.

The VRMode action is responsible for the management of display modalities within the proposed environment. In order to truly enable customisation and experimentation with devices and input mappings, an option to switch between playing with a headmounted display (HMD) in VR or interacting with the scene from a standard monitor was necessary. The example mapping for VRMode consists of two alternatives:

• The Default option starts up the scene in VR mode, meaning that the user needs to be using a head-mounted headset. These include the HTC VIVE Pro Eye Headset, the Meta Quest 2 Headset, and the Meta Quest Pro Headset. When this Configuration is active, the user’s height, head
position and rotation is actively tracked.

• The Disabled option turns off any VR device connected, and the main monitor becomes the source of information where the scene is displayed, effectively transforming the Unity scene from VR to non-VR.

The Locomotion Action represents the set of inputs that control the camera rotation and in-game movement. In other words, the user’s point of view, and motion mirrors the input data from the Locomotion Action. The example mapping for Locomotion consists of four alternatives:

• The Default option tracks the user’s movement through the left VR controller trackpad data, and the camera rotation via the right VR controller trackpad data. This Configuration supports three types of VR controller: the HTC VIVE Pro Eye Wands, the Meta Quest 2 Controllers, and the Meta Quest Touch Pro Controllers.

• The Inverted option matches the logic of the Default Configuration, but tracks the user’s movement with the right VR controller trackpad data, while the left VR controller trackpad data tracks the camera rotation.

• The Gamepad option allows the user to rotate the camera through the use of the right analog joystick data, and move in the scene with the left analog joystick data. Any gamepad is supported for this Configuration, but for this project’s context, both the Xbox Wireless Controller and the Playstation DualSense Controller are accepted.

• Similar to the Gamepad option, Gamepad Inverted will allow the user to move and rotate the camera through the use of the connected gamepad’s joysticks, but in this case, the right analog joystick data will process in-game movement, while the left joystick data will be responsible for the camera rotation. This Configuration and Inverted are included to showcase how the system supports both device switching, but also different input mappings for each device. The Interaction Action represents the mapping requirements that allow the user to pick up and/or interact with objects in the VR scene. The data needed to do so is the
user’s virtual hand position, and three digital inputs, buttons or triggers, that represent user interaction with the virtual environment. Four default mapping alternatives are available for the Interaction Action:

• The Default option will use the index trigger to activate objects, the grip button to pick them up, and wand position and wand rotation data to establish the in-game position of the user’s hand. The menu button will be used to toggle the Control Interface Manager UI. This mapping will be activated for both hands, allowing the user to do all the above on both controllers. This Configuration supports three types of VR controllers: the HTC VIVE Pro Eye Wands, the Meta Quest 2 Controllers, and the Meta Quest Touch Pro Controllers.

• The Right Hand Only option uses the same logic as the Default option, but disables interaction with the left VR controller.

• The Left Hand Only option uses the same logic as the Default option, but disables interaction with the right VR controller.

• The Gamepad option adopts the right trigger to activate objects, the left trigger to pick them up, and the south button to toggle the Control Interface Manager UI. The south button is the A button on the Xbox Wireless Controller or the X button on the PlayStation DualSense Controller. Because there is no spatial tracking for gamepads, the user’s hand in the VR scene will be locked so that it is visible from the camera, and the interactivity remains unchanged.

The Action System reflects the Input System’s design, in which the logical meaning of input is separated from the physical device producing the input. To summarise, Actions are mapping groups that represent an interaction option for the user. They are not the implemented features that allow the user to perform the described activities but interfaces for developers to adopt without having to know what device is being used.
The existing Unity Input System is used to create a new Action: it will allow the user to provide the Action name and the list of controls that need to be mapped to physical inputs. Some Actions might require more than just one mapping option for specific types of devices with data constraints. An example would be having a Configuration in which you can toggle the Control Interface Manager’s UI with more than one button, or with the simultaneous press of two buttons at the same time. Hence, the Action class will allow for more than one mapping structure to be defined.

The naming format for Actions and Configurations within the Input Actions Asset is: *ActionName-ConfigurationName*. This will allow the UI to correctly identify the set of Actions included in the system and the related Configurations for each Action. 

Unity’s XR Interaction Toolkit contains a series of ready-to-use scripts and components that allowed for a simple and quick implementation of the basic VR mechanics described above. For each activity that requires input from the user, a reference to an Action and a mapping is required. An issue arose when trying to switch between Configurations from the Control Interface Manager’s UI: the pre-made scripts do not account for dynamic switching of Actions, as the initial design of the Input System was not intended to be operated in such a fashion. The solution was to implement a listener component that would detect any change happening in the Input Actions Asset, and switch Configurations as a result. The listener components can be found in this GitHub repository, within the Scripts folder, and are labeled with the CIM naming.
