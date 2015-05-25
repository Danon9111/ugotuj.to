import java.lang.Throwable;
import cucumber.api.java.en.*;

/**
 * Created by andrz_000 on 2015-05-26.
 */
public class steps {
    @Then("^Error message is visible$")
    public void Error_message_is_visible() throws Throwable {
        // Express the Regexp above with the code you wish you had
        throw new cucumber.api.PendingException();
    }

    @Given("I open the registration page")
    public void I_open_the_registration_page() throws Throwable {
        throw new cucumber.api.PendingException();
    }
}
