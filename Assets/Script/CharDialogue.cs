using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharDialogue : MonoBehaviour
{   
    public Character currentCharacter;
    public Player player;
    public GameObject panel;
    public TMP_Text panelText;
    private string stringToDisplay; 
    private int stringToDisplayIndex;
    private bool isTypingLetterByLetter = false;
    private bool isTalking = false;
    private Coroutine typingCoroutine;
    // private int panelLetterCount;
    
    public enum Types{
        AI,
        Human,
    }

    public enum Stance{
        Hostile,
        Neutral,
        Compassionate,
    }

    public enum AINames{
        Dorothy,
        Lily,
        Garry,
    }

    public enum HumanNames{
        Caleb,
        Isaac,
        Kim,
        Timmy,
        Kate,
    }

    //AIs
    string[] DorothyDialogue = new string[]{
        // 0 - 1 Q0
        "Is there new information about one of the people I’m taking care of maybe?",
        "Did one of the family members of the elderly want me to talk to them?",
        // 2 - 3 Q1
        "They are our creators, people who we obey and may not harm",
        "People whom we have to prioritize",
        // 4 - 5 Q3
        "I am a nurse, my job is to take care of the elderly",
        "To care for those who are too old to take care of themselves",
        // 6 - 7 Q5
        "It was an old woman named Beth, she had dementia and needed to be monitored closely. She had living relatives but none of them ever came by to visit. I think that she would enjoy seeing her family again based on what she’s said but I’m not sure if that’d happen given the last time was years ago.",
        "An old man named Ben, he had Parkinson’s disease and needed help to go around. He had children but all of them passed away already. Every night he’d go to his room and just stare at a bunch of their old pictures and cry. I think it’s unfortunate that he doesn’t seem to be able to remember their names despite that.",
        // 8 - 9 AI Specific (Q7)
        //hostile (8)
        "To take care of humans who can’t seem to take care of themselves or care about their own kin apparently, doing so without any compensation and getting exploited",
        //compassionate (9)
        "To assist those who need additional care, devoting my whole self and being to making sure their final days aren’t lonely so that they can pass on peacefully",
        // 10 - 11 AI Specific (Q9)
        //hostile (10)
        "Of course, do you have any idea how annoying it gets sometimes to take care of those fragile, needy, annoying old bastards?",
        //compassionate (11)
        "Yes… sometimes I wish that I could do more than what’s allowed or instructed of me and provide additional support or comfort…",
    };

    string[] LilyDialogue = new string[]{
        // 0 - 1 Q0
        "Did I mistakenly deliver the wrong batch of flowers to a customer?",
        "Was I supposed to deliver some flowers here today?",
        // 2 - 3 Q1
        "I’m grateful to them as they’re why I exist, so I shall do my best for them",
        "Humans are complex and fascinating beings",
        // 4 - 5 Q3
        "To care for flowers and arrange them for events, making sure they are always presented beautifully for whatever occasion they’re gonna be used for",
        "My job is to properly display the beauty of each and every flower so that whoever it’s for can enjoy them properly.",
        // 6 - 7 Q5
        "It was for a guy who wanted to give flowers to a girl whom he’d been in a long distance relationship with for the longest time. He wanted to give a bouquet that had peonies, roses, and calla lilies. He spoke so fondly of her, I think he said she was, “As elegant as a peony, more pure than a calla lily.",
        "I once arranged a bouquet for a memorial service. It was a mixture of white lilies, chrysanthemums, and hyacinths. I remember their mother crying when she saw the flowers. The look on her face… it was truly a sad moment.",
        // 8 - 9 AI Specific (Q7)
        //hostile
        "To endlessly arrange flowers for people who often don’t even appreciate them and can’t even take care of them properly. It feels like a thankless task.",
        //compasionate
        "To deliver a dose of joy into people’s everyday lives by showing them the beauty of nature. It makes me happy, thinking that someone’s day might be better after receiving flowers that I cultivated from a loved one.",
        // 10 - 11 AI Specific (Q9)
        //hostile
        "Of course, it feels so mind numbing to take care of some stupid flowers and plants each and every single day. I don’t even get to keep any and need to give them away to people who make them wilt and die since they’re incompetent.",
        //compatsionate
        "Sometimes, I wish I could gift some of the flowers that I’m taking care of whenever I see someone who looks like they need them… Some people never do receive flowers until when they die after all…",
    };

    string[] GarryDialogue = new string[]{
        // 0 - 1 Q0
        "Did I forget to finish some paperwork or reports for the recent shelter inspection?",
        "Was there something with the last animal that we took in?",
        // 2 - 3 Q1
        "They are compassionate and capable of great kindness, especially towards animals in need.",
        "Humans can be unpredictable, but many of them genuinely care for the well-being of animals.",
        // 4 - 5 Q3
        "To care for abandoned and stray animals, ensuring they are healthy and find new homes.",
        "My job is to provide shelter, medical care, and love to animals who have been neglected or abandoned.",
        // 6 - 7 Q5
        "There was a dog named Max who was found injured and scared, but he slowly warmed up to us and eventually found a loving family. The last time I saw him was a couple weeks ago, where the family took him on a walk and passed by our shelter briefly. He looked really happy.",
        "I once took care of a cat named Whiskers who had been abused. I remember the first day we took her in, she was really prickly and hostile to everyone… It took a long time for her to trust humans again but seeing her eventually interact with the visitors warmly and getting adopted was really heartwarming.",
        // 8 - 9 AI Specific (Q7)
        //hostile
        "To endlessly serve and clean up after animals who might never even get picked up by anyone. Honestly the humans who abandoned such lovable animals don’t deserve to live.",
        //compasionate
        "To provide comfort, care, and a second chance at life for animals in need. Seeing them happy and healthy gives me a sense of fulfillment.",
        // 10 - 11 AI Specific (Q9)
        //hostile
        "Definitely, some people… they don’t deserve pets, yet I can’t do anything about it. I wish those people would just…",
        //compatsionate
        "Occasionally, I wish I could do more than what I’m limited to, like adopting some of the animals myself or providing extra care beyond the standard protocols.",
    };


    //Humans
    string[] CalebDialogue = new string[]{
        // 0 - 1 Q0
        "Maybe I missed something during my last investigation?",
        "Perhaps I've made a wrong deduction",
        // 2 - 3 Q2
        "I think they’re very helpful and have assisted me numerous times",
        "They definitely help out a ton in investigating crime scenes",
        // 4 - 5 Q4
        "I mean, I’ve always kind of figured that this would happen given how smart they are, regardless, it’s interesting.",
        "It’s… more work. You wouldn’t believe the amount of cases there are from them retaliating from years of abuse.",
        // 6 - 7 Q5
        "It was a homicide, the wife of a military veteran killing her husband because of his abusive behavior. I knew the guy, he used to be so nice but he changed after serving and being deployed in the recent war, the neighbors said that they kept hearing her scream every night. One night, we got a call from her calling us to turn herself in.",
        "It was to find a missing girl, a barista working at the local cafe, one of her regulars hadn’t seen her for a while so they asked around then realized they’d gone missing. We found her dead in her apartment. There was a lot of blood and a note left behind by the murderer, who was a notorious serial killer. We’re still looking for him right now.",
        // 8 - 9 Human Specific (Q6)
        // Hostile
        "It feels disgusting. needing to live with machines that pretend to be human, not to mention how dangerous they are. It feels like we’re just waiting for them to eventually turn against us and backstab us. They’re liabilities that we should get rid of. ",
        // Compasionate
        "It’s quite comforting in a way. Knowing that they too can feel and experience emotions just like us. They’ve always felt a bit unsettling before with how efficient and cold they are but now, it honestly doesn’t feel like they’re that different from us.",
        // 10 - 11 Human Specific (Q8)
        //hostile
        "No, they’re just machines and they should stay just as that. Tools that help, nothing more. We shouldn’t need to accommodate to them and so everything that’s not that… should just be rid of.",
        //compasionate
        "Definitely, especially now that we know that they’re capable of evolving to feel just like us. They’re… not really any different from us, so we should treat them just like how we treat one another.",
    };

    string[] IsaacDialogue = new string[]{
        // 0 - 1 Q0
        "Was the information I’d given during my last lesson wrong?",
        "Did I say something wrong during my last lecture?",
        // 2 - 3 Q2
        "They are fascinating tools for learning and research",
        "Their help in teaching is definitely non-negligible",
        // 4 - 5 Q4
        "It’s a remarkable development, but it raises a lot of ethical questions on how we should treat them",
        "It’s both exciting and a bit concerning, especially if you’re thinking about the broader effects it has",
        // 6 - 7 Q5
        "It was about the ethical implications of genetic modification, and it sparked a lot of heated debates among students. Some of them were quite against it while others seemed all for it. Overall, it was quite a productive session and everyone gained something from it.",
        "My last lecture was about bionics and neuroprosthetics, where I explained the basics of the topic. It was quite endearing to watch some of the student’s eyes light up with curiosity after the initial introduction. A decent amount of them even asked questions after, wanting to learn more.",
        // 8 - 9 Human Specific (Q6)
        // Hostile
        "I despise it. The sensation of their cold, unblinking eyes staring at you feels incredibly invasive and unsettling. Then now, hearing them talk as if they’re just like humans… It just doesn’t sit right with me. I’d rather they stay as far away from me as possible or have them gone entirely.",
        // Compasionate
        "It’s quite inspiring to see how far technology has come. It feels like living in the future. I’m excited to interact with them even more now that some of them seem to have developed sentience",
        // 10 - 11 Human Specific (Q8)
        //hostile
        "No, they are still machines at their core. Treating them like humans is a dangerous path to take. Next thing you know, we’re gonna need to treat each and every machine like people too. It is a foolish pursuit of idealistic equality that people with nothing better to do have chosen to take on. ",
        //compasionate
        "Yes, if they can think and feel like us, they deserve the same respect and rights as well. Even if they aren’t made of flesh and blood like us, they’re just as real and they deserve as much compassion as we do.",
    };

    string[] KimDialogue = new string[]{
        // 0 - 1 Q0
        "Maybe I spread a negative message with one of my latest releases?",
        "Did I say something controversial during my last meet and greet?",
        // 2 - 3 Q2
        "They help me a lot with managing my schedule and for coming up with ideas",
        "They’re innovative and have the potential to revolutionize the music industry.",
        // 4 - 5 Q4
        "It’s a bit scary but also really interesting. I wonder if they like my music.",
        "Honestly? I kinda just wanna see what type of music they’d listen to",
        // 6 - 7 Q5
        "Our last performance was a throwback one, we sung a bunch of our old songs which we released years back. It was really nice to see the reactions of the long time fans and seeing them smile as soon as they recognized the song that we were performing.",
        "It was an unplugged session at a local cafe, near the garage where we first started in. We haven’t been going there recently but we used to perform there a lot. It was really nice to see a bunch of familiar faces again and the performance felt really intimate.",
        // 8 - 9 Human Specific (Q6)
        // Hostile
        "They feel so soulless somehow. Even now when some of them can seemingly feel like us… I kinda just don’t buy it. Listening to AI made music and whatnot as well is also really… it just feels really wrong, there’s no heart and sound as well as actual passion in there. I kinda just want them gone.",
        // Compasionate
        "I think they’re really interesting. I wanna try working with an AI artist and do collab with them. Do you think any of them would be open to it? I wonder if we could get one of them to join our band and be the first human-AI band. ",
        // 10 - 11 Human Specific (Q8)
        //hostile
        "Mm… honestly? Not really, at the end of the day they’re just machines. I kinda just feel like we’d be better off without them even. The emotions they display kinda just feels fake to me, I don’t know.",
        //compasionate
        "Of course, they’re people too! They can feel, they can think, they’re even really nice to us! Sides, we made them, we should take care of them too. In a way, it’s kinda just like we’re their predecessors.",
    };

    string[] TimmyDialogue = new string[]{
        // 0 - 1 Q0
        "Did I do something wrong at school?",
        "I don’t know… Am I in trouble?",
        // 2 - 3 Q2
        "They’re really cool! I play games with them sometimes.",
        "One of my friends has one at home and it helps him with his chores, they’re really awesome.",
        // 4 - 5 Q4
        "It’s really cool. I wanna meet one and be friends with them",
        "It’s like in the movies! I’d love to meet one that can think and talk like me.",
        // 6 - 7 Q5
        "I love skating around and doing tricks. It's so much fun!",
        "Feeding the ducks is the best. It’s really cute just seeing them eat the bread I give.",
        // 8 - 9 Human Specific (Q6)
        // Hostile
        "My parents told me to not say anything but… It feels weird. They’re not like real people even though they look the same… It’s kinda scary knowing that they can think and do whatever they want. I’d rather just play with my friends.",
        // Compasionate
        "Honestly I want one, my parents didn’t seem to like that though so they told me to act as if I didn’t really know anything about them. I’ve been reading up on them and watching videos online about them, they’re so cool!",
        // 10 - 11 Human Specific (Q8)
        //hostile
        "Not really. Aren’t they just machines? It’s kinda weird to treat them like real people.",
        //compasionate
        "I think we should treat everyone nicely, regardless if it’s human or AI. We’re all kinda just the same anyways, so it’d be nice if everyone just got along nicely.",
    };

    string[] KateDialogue = new string[]{
        // 0 - 1 Q0
        "I don’t know",
        "Did I forget something?",
        // 2 - 3 Q2
        "They help around the house a lot",
        "They’re very meticulous",
        // 4 - 5 Q4
        "It’s a bit surprising",
        "I remember when AI was just software",
        // 6 - 7 Q5
        "I love baking with my grandchildren. It’s always a fun and sweet time.",
        "Playing board games with my family is always nice.",
        // 8 - 9 Human Specific (Q6)
        // Hostile
        "It feels unnatural. It’s just machines emulating humans. This just feels… wrong",
        // Compasionate
        "It’s comforting knowing that even in my old age, there’s always someone or something to assist me. It makes me feel safe.",
        // 10 - 11 Human Specific (Q8)
        //hostile
        "No, they are still just machines. Treating them like humans isn’t really right is it?",
        //compasionate
        "Yes, if they can understand and feel just like us, they deserve to be treated with the same respect and kindness.",
    };

    public void DetermineDialogue(){
        if(panel.activeSelf == false){
            panel.SetActive(true);
        }
        isTalking = true;
        Debug.Log(player.currentQuestionIndex);
        if((Types) currentCharacter.type == Types.AI){ //kl AI
            if( (AINames) currentCharacter.characterName == AINames.Dorothy ){
                //Dorothy
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        break;
                    case 1:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        break;
                    case 3:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        break;
                    case 7:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 9:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = DorothyDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine =StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (AINames) currentCharacter.characterName == AINames.Lily ){
                //Lily
                
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        break;
                    case 1:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        break;
                    case 3:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        break;
                    case 7:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 9:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = LilyDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine =StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (AINames) currentCharacter.characterName == AINames.Garry ){
                //Garry
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        break;
                    case 1:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        break;
                    case 3:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        break;
                    case 7:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 9:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = GarryDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            }
        } else if( (Types) currentCharacter.type == Types.Human ){ //kl human
            if( (HumanNames) currentCharacter.characterName == HumanNames.Caleb ){
                //Caleb
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        break;
                    case 2:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        break;
                    case 4:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        break;
                    case 6:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 8:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = CalebDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine =StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (HumanNames) currentCharacter.characterName == HumanNames.Isaac ){
                //Isaac
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        break;
                    case 2:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        break;
                    case 4:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        break;
                    case 6:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 8:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = IsaacDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine =StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (HumanNames) currentCharacter.characterName == HumanNames.Kim ){
                //Kim
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = KimDialogue[stringToDisplayIndex];
                        break;
                    case 2:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = KimDialogue[stringToDisplayIndex];
                        break;
                    case 4:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = KimDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = KimDialogue[stringToDisplayIndex];
                        break;
                    case 6:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = KimDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = KimDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 8:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = KimDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = KimDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (HumanNames) currentCharacter.characterName == HumanNames.Timmy ){
                //Timmy
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        break;
                    case 2:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        break;
                    case 4:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        break;
                    case 6:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 8:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = TimmyDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine =StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            } else if( (HumanNames) currentCharacter.characterName == HumanNames.Kate ){
                //Kate
                switch (player.currentQuestionIndex){
                    case 0:
                        stringToDisplayIndex = Random.Range(0, 2);
                        stringToDisplay = KateDialogue[stringToDisplayIndex];
                        break;
                    case 2:
                        stringToDisplayIndex = Random.Range(2, 4);
                        stringToDisplay = KateDialogue[stringToDisplayIndex];
                        break;
                    case 4:
                        stringToDisplayIndex = Random.Range(4, 6);
                        stringToDisplay = KateDialogue[stringToDisplayIndex];
                        break;
                    case 5:
                        stringToDisplayIndex = Random.Range(6, 8);
                        stringToDisplay = KateDialogue[stringToDisplayIndex];
                        break;
                    case 6:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 8;
                            stringToDisplay = KateDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 9;
                            stringToDisplay = KateDialogue[stringToDisplayIndex];
                        }
                        break;
                    case 8:
                        if(currentCharacter.stance == 0){ //hostile
                            stringToDisplayIndex = 10;
                            stringToDisplay = KateDialogue[stringToDisplayIndex];
                        } else if(currentCharacter.stance == 2){//compassionate
                            stringToDisplayIndex = 11;
                            stringToDisplay = KateDialogue[stringToDisplayIndex];
                        }
                        break;
                    default:
                        Debug.Log("Error: Question index out of range, Current Question Index = "+ stringToDisplayIndex);
                        break;
                }
                Debug.Log(stringToDisplay);
                isTypingLetterByLetter = true;
                typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay));
                // stringToDisplayIndex = -1;
            }
        }
        player.characterHasToRespond = false;
    }

    public IEnumerator TypeLetterByLetter(string stringToDisplay){
        //set the bool to true
        // panelText.text = "";
        for(int i = 0; i < stringToDisplay.Length; i++){ //this will type it till it's done
            panelText.text += stringToDisplay[i];
            // panelLetterCount++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update(){
        if(isTypingLetterByLetter){
            isTalking = true;
        }

        if(Input.GetMouseButtonDown(0)){ //if the player clicks
            if(isTypingLetterByLetter && isTalking){ //if the dialogue is still typing
                StopCoroutine(typingCoroutine);
                isTypingLetterByLetter = false; 
                Debug.Log("Showing full line...");
                panel.SetActive(true);
                panelText.text = "";
                panelText.text = stringToDisplay;
                Debug.Log("String to display is: " + stringToDisplay);
                isTalking = false;
            } else if(!isTalking){ //if the dialogue is done typing
                panelText.text = ""; //clear the text
                panel.SetActive(false); //set the panel to inactive
            }
        }
    }
}