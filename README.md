# Find OffMeshLinks in a NavMeshAgent Path

In this short tutorial repository and [accompanying video](https://youtu.be/PzUsjEB2cQ4) you'll learn how to find the OffMeshLinks (high level component: NavMeshLink) that a NavMeshAgent will take on their current path.
Unfortunately, Unity does not provide a way to query the NavMesh for OffMeshLinks, so it's not possible to find ALL OffMeshLinks (unless you have all OffMeshLinks as NavMeshLinks and do something like `FindObjectsOfType󠀼󠀼<NavMeshLink>()`).

Using this method, regardless if you're using a `NavMeshLink` or `OffMeshLink`, you can find the exact points that a NavMeshAgent will traverse an OffMeshLink between!

As always, all code from this video is available on GitHub: https://github.com/llamacademy/ai-series-part-43

As usual with the AI Series, we're using the NavMesh Components: https://docs.unity3d.com/Manual/NavMesh-BuildingComponents.html not the built-in navigation system. 

[![Youtube Tutorial](./Video%20Screenshot.jpg)](https://youtu.be/PzUsjEB2cQ4)

## Patreon Supporters
Have you been getting value out of these tutorials? Do you believe in LlamAcademy's mission of helping everyone make their game dev dream become a reality? Consider becoming a Patreon supporter and get your name added to this list, as well as other cool perks.
Head over to https://patreon.com/llamacademy to show your support.

### Phenomenal Supporter Tier
* Andrew Bowen
* YOUR NAME HERE!

### Tremendous Supporter Tier
* YOUR NAME HERE!

### Awesome Supporter Tier
* Gerald Anderson
* AudemKay
* Matt Parkin
* Ivan
* Paul Berry
* Reulan
* YOUR NAME HERE!

### Supporters
* Bastian
* Trey Briggs
* Matt Sponholz
* Dr Bash
* Tarik
* EJ
* Chris B.
* Sean
* YOUR NAME HERE!

## Other Projects
Interested in other AI Topics in Unity, or other tutorials on Unity in general? 

* [Check out the LlamAcademy YouTube Channel](https://youtube.com/c/LlamAcademy)!
* [Check out the LlamAcademy GitHub for more projects](https://github.com/llamacademy)

## Socials
* [YouTube](https://youtube.com/c/LlamAcademy)
* [Facebook](https://facebook.com/LlamAcademyOfficial)
* [TikTok](https://www.tiktok.com/@llamacademy)
* [Twitter](https://twitter.com/TheLlamAcademy)
* [Instagram](https://www.instagram.com/llamacademy/)
* [Reddit](https://www.reddit.com/user/LlamAcademyOfficial)

## Requirements
* Requires Unity 2020.3 LTS or higher.
* [Navigation Components](https://docs.unity3d.com/Manual/NavMesh-BuildingComponents.html)