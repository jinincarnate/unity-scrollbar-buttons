# unity-scrollbar-buttons
A naive approach to implement smooth scrolling on Unity's Scroll Rect with buttons as unity doesn't provide buttons on its scroll bar

### Features
1. Should work with all unity versions.
2. You can add buttons to scroll the contents of a unity scroll rect instead of just using mouse scroll or dragging the scroll bar or the content itself.
3. Buttons will get disabled when you reach the end of the content or basically when you can not scroll anymore either up or down.
4. Updates the button's state (that is enabling or disabling them) on runtime even if the content is added or removed dynamically.


### Caveats
1. While holding the button the smooth scrolling logic sometimes makes the scroll bar to slow down considerably when its about to reach the end.

### How to use
1. Create a new unity scroll in the scene.
2. Add 'ScrollElementsContainer' component to the Scroll Rect's Content transform.
3. Add two new buttons (that will be used for scrolling), most likely under the scroll rect(but doesn't matter)
4. Add 'ScrollButton' component to both the buttons and assign all the exposed fields.
   - choose Direction as UP or one button and DOWN for other
   - assign step size as 1(tested with 1 you can experiment)
   - assign scroll frequency as 1(basically if you hold the button how frequently it should take the scroll input)
   - assign the scroll bar value by dragging and dropping the scroll bar for which you are adding the buttons.
   - assign the Scroll elements container value by dragging and dropping the Scroll Rect's Content transform.(which already should have the 'ScrollElementsContainer' component)
   
   **Note: A Sample scene is included in the scenes folder, please refer for the default values that must be assigned in case the 'How to use' doesn't make sense.**
  
